using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WordenaServerEF
{
    public class WordenaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Phrase> Phrases { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class Character
    {
        [Key]
        public int CharId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int UserId { get; set; }
    }

    public class Phrase
    {
        [Key]
        public int PhraseId { get; set; }
        public string Sentence { get; set; }
        public int Level { get; set; }
    }
}
