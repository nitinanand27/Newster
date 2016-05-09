namespace news.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednotaion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Heading", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Articles", "Text", c => c.String(nullable: false, maxLength: 140, storeType: "nvarchar"));
            AlterColumn("dbo.Articles", "SourceAdress", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Comments", "Text", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(unicode: false));
            AlterColumn("dbo.Users", "Email", c => c.String(unicode: false));
            AlterColumn("dbo.Users", "UserName", c => c.String(unicode: false));
            AlterColumn("dbo.Comments", "Text", c => c.String(unicode: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(unicode: false));
            AlterColumn("dbo.Articles", "SourceAdress", c => c.String(unicode: false));
            AlterColumn("dbo.Articles", "Text", c => c.String(unicode: false));
            AlterColumn("dbo.Articles", "Heading", c => c.String(unicode: false));
        }
    }
}
