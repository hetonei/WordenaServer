using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WordenaData;
using WordenaData.Data;

namespace WordenaServerEF.CommandProcessing
{
    public class Commands : ICommand
    {


        //Login,,Password-List<DCharacter>
        public string LogIn(string data)
        {
            Console.WriteLine("Выполняется команда LOGIN...");
            List<String> logpass = Separator.Data;
            string reply;
            if (DUser.IsLoggedIn(logpass[0], logpass[1]))
            {
                string temp = "";
                foreach (var user in DUser.Items.Values.ToArray())
                {
                    if (user.UserId == DUser.GetUserId(logpass[0]))
                    {
                        temp = JsonConvert.SerializeObject(user.Chars);
                        break;
                    }
                }
                reply = temp;
                Console.WriteLine("Команда выполнена! Клиенту отправлен список персонажей пользователя " + logpass[0]);
            }
            else
            {
                reply = "0";
                Console.WriteLine("Команда выполнена! Нет сходства {0}:{1} ", logpass[0], logpass[1]);
            }
            return reply;
        }

        //DUser-DUser
        public string NewUser(string data)
        {
            Console.WriteLine("Выполняется команда NewUser...");
            DUser tempUser = JsonConvert.DeserializeObject<DUser>(Separator.Data[0]);
            TableManager.AddUser(tempUser.UserId, tempUser.UserLogin, tempUser.UserPassword);
            TableManager.lbRefresh();
            string reply = JsonConvert.SerializeObject(tempUser);
            Console.WriteLine("Пользователь {0}.{1} добавлен!", tempUser.UserLogin, tempUser.UserPassword);
            return reply;
        }

        //DCharacter-List<DCharacter>
        public string NewChar(string data)
        {
            string reply = "";
            Console.WriteLine("Выполняется команда NewChar...");
            DCharacter tempChar = JsonConvert.DeserializeObject<DCharacter>(Separator.Data[0]);
            TableManager.AddChar(tempChar.CharId, tempChar.CharName, tempChar.CharLevel, tempChar.CharHp, tempChar.CharMp, tempChar.UserId);
            TableManager.lbRefresh();
            var uid = TableManager.GetUserId(tempChar.CharName);
            foreach (var u in DUser.Items.Values.ToList())
            {
                if (u.UserId == uid) reply = JsonConvert.SerializeObject(u.Chars);
            }
            
            Console.WriteLine("Персонаж {0}.{1} добавлен!", tempChar.CharId, tempChar.CharName);
            return reply;
        }

        //DCharacter-List<DCharacter>
        public string RemoveChar(string data)
        {
            Console.WriteLine("Выполняется команда RemoveChar...");
            DCharacter tempChar = JsonConvert.DeserializeObject<DCharacter>(Separator.Data[0]);
            var id = tempChar.UserId;
            TableManager.RemoveChar(tempChar.CharId, tempChar.CharName, tempChar.CharLevel, tempChar.CharHp, tempChar.CharMp, id);
            TableManager.lbRefresh();
            string reply = "";
            foreach (var user in DUser.Items.Values.ToArray())
            {
                if (user.UserId == id) reply = JsonConvert.SerializeObject(user.Chars);
            }
            Console.WriteLine("Персонаж {0}.{1} удалён!", tempChar.CharId, tempChar.CharName);
            return reply;
        }

        //Уровень-DPhrase
        public string StartGame(string data)
        {
            Random r = new Random();
            var tempList = new List<DPhrase>();
            foreach (var ph in DPhrase.Items.Values.ToList())
            {
                if(ph.Level==Convert.ToInt32(Separator.Data[0])) tempList.Add(ph);
            }
            DPhrase selectedPhrase = tempList.ElementAt(r.Next(0, tempList.Count));
            string reply = JsonConvert.SerializeObject(selectedPhrase);
            return reply;
        }

        //DResult-String
        //Клиент while(true) шлёт запрос на GetResult()
        public string CatchResults(string data)
        {
            string reply = "1";
            var tempResult = JsonConvert.DeserializeObject<DResult>(Separator.Data[0]);
            if (tempResult.Truly)
            {
                DResult.Items.Values.ToList().Add(tempResult);
                DResult.Items.Values.ToList().Add(tempResult);
            }
            else
            {
                reply = "Вы програли! Неверно введено слово!";
            }
            return reply;
        }

        //Whatever-UserId
        public string GetResult(string data)
        {
            string reply = "";
            var resultArray = DResult.Items.Values.ToArray();
            DateTime dt = new DateTime();
            if (resultArray.Length > 1)
            {
                dt = resultArray[0].InputTime;
                for (int i = 1; i < resultArray.Length; i++)
                {
                    if (resultArray[i].InputTime < dt) dt = resultArray[i].InputTime;
                }
                foreach (var res in resultArray)
                {
                    if (dt == res.InputTime) reply = Convert.ToString(res.UserId);
                }
            }
            else reply = "420";
            return reply;
        }
    }
}
