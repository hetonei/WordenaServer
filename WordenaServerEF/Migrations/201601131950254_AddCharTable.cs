namespace WordenaServerEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCharTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        CharId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Level = c.Int(nullable: false),
                        HP = c.Int(nullable: false),
                        MP = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CharId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Characters");
        }
    }
}
