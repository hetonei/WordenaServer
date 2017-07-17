namespace WordenaServerEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Login", c => c.String());
            AddColumn("dbo.Users", "Password", c => c.String());
            DropColumn("dbo.Users", "UserLogin");
            DropColumn("dbo.Users", "UserPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserPassword", c => c.String());
            AddColumn("dbo.Users", "UserLogin", c => c.String());
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Login");
        }
    }
}
