using System;
using System.Linq;
using WordenaData;
using WordenaData.Data;

namespace WordenaServerEF
{
    public class TableManager
    {
        public static void ClearObjLists()
        {

            DUser.Items.Clear();
            DCharacter.Items.Clear();
            DPhrase.Items.Clear();
        }
        public static void DbSynchronize()
        {
            
            using (var db = new WordenaContext())
            {
                Console.WriteLine("Получение данных из таблицы {0}...", db.GetTableName<Character>());
                foreach (var ch in db.Characters)
                {
                    DCharacter.Items.Values.ToList().Add(
                        new DCharacter(
                            ch.CharId,
                            ch.Name,
                            ch.Level,
                            ch.UserId,
                            ch.HP,
                            ch.MP));
                }
                Console.WriteLine("Данные получены!");
                Console.WriteLine("Получение данных из таблицы {0}...", db.GetTableName<User>());
                foreach (var u in db.Users)
                {
                    DUser.Items.Values.ToList().Add(
                        new DUser(
                            u.UserId,
                            u.Login,
                            u.Password
                            ));
                }
                Console.WriteLine("Данные получены!");
                Console.WriteLine("Получение данных из таблицы {0}...", db.GetTableName<Phrase>());
                foreach (var ph in db.Phrases)
                {
                    DPhrase.Items.Values.ToList().Add(
                        new DPhrase(
                            ph.PhraseId,
                            ph.Sentence,
                            ph.Level
                            ));
                }
                Console.WriteLine("Данные получены!");
            }
        }

        public static void ChangeUserId()
        {
            using (var db = new WordenaContext())
            {
                foreach (var ch in db.Characters)
                {
                    if (ch.UserId == 0) ch.UserId = 3;
                    if (ch.UserId == 1) ch.UserId = 2;
                }
                db.SaveChanges();
            }
        }

        public static int GetUserId(string name)
        {
            using (var db = new WordenaContext())
            {
                foreach (var ch in db.Characters)
                {
                    if (ch.Name == name) return ch.UserId;
                }
                return 0;
            }
        }

        public static void AddPhrase(string sentence, int lvl)
        {
            using (var db = new WordenaContext())
            {
                db.Phrases.Add(new Phrase {Sentence = sentence, Level = lvl});
                db.SaveChanges();
            }
        }

        public static void RemovePhrase(string sentence, int lvl, int id)
        {
            using (var db = new WordenaContext())
            {
                var phrase = new Phrase {Sentence = sentence, Level = lvl, PhraseId = id};
                db.Phrases.Attach(phrase);
                db.Phrases.Remove(phrase);
                db.SaveChanges();
            }
        }

        public static void lbRefresh()
        {
            ClearObjLists();
            DbSynchronize();
        }

        public static void AddUser(int id, string log, string pass)
        {
            using (var db = new WordenaContext())
            {
                db.Users.Add(new User {Login = log, Password = pass});
                db.SaveChanges();
            }
            
        }

        public static void AddChar(int id, string name, int lvl, int hp, int mp, int ui)
        {
            using (var db = new WordenaContext())
            {
                db.Characters.Add(new Character{Name = name, Level = lvl, HP = hp, MP = mp, UserId = ui});
                db.SaveChanges();
            }
        }

        public static void RemoveUser(int id, string log, string pass)
        {
            using (var db = new WordenaContext())
            {
                var user = new User {Login = log, Password = pass};
                db.Users.Attach(user);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public static void RemoveChar(int id, string name, int lvl, int hp, int mp, int uid)
        {
            using (var db = new WordenaContext())
            {
                var character = new Character {Name = name, HP = hp, Level = lvl, MP = mp, UserId = uid};
                db.Characters.Attach(character);
                db.Characters.Remove(character);
                db.SaveChanges();
            }
        }

    }
}
