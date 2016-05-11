namespace news.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedconfirmedtouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Confirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Confirmed");
        }
    }
}
