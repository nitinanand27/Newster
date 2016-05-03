namespace news.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedarticle : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Articles", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Author", c => c.Int(nullable: false));
        }
    }
}
