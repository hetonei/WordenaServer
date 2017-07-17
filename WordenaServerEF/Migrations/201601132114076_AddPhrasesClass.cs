namespace WordenaServerEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhrasesClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Phrases",
                c => new
                    {
                        PhraseId = c.Int(nullable: false, identity: true),
                        Sentence = c.String(),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhraseId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Phrases");
        }
    }
}
