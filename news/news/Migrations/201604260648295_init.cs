namespace news.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.Int(nullable: false),
                        Heading = c.String(nullable: false, unicode: false),
                        Text = c.String(nullable: false, maxLength: 140, unicode: false, storeType: "nvarchar"),
                        Date = c.DateTime(nullable: false, precision: 0),
                        Source = c.String(nullable: false, unicode: false),
                        ImgAdress = c.String(unicode: false),
                        VideoAdress = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author, cascadeDelete: true)
                .Index(t => t.Author);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        CreatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId, cascadeDelete: true)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, unicode: false),
                        Email = c.String(nullable: false, unicode: false),
                        Password = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryArticles",
                c => new
                    {
                        Category_CategoryId = c.Int(nullable: false),
                        Article_ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryId, t.Article_ArticleId })
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Article_ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "Author", "dbo.Users");
            DropForeignKey("dbo.Categories", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.CategoryArticles", "Article_ArticleId", "dbo.Articles");
            DropForeignKey("dbo.CategoryArticles", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Articles", new[] { "Author" });
            DropIndex("dbo.Categories", new[] { "CreatorId" });
            DropIndex("dbo.CategoryArticles", new[] { "Article_ArticleId" });
            DropIndex("dbo.CategoryArticles", new[] { "Category_CategoryId" });
            DropTable("dbo.CategoryArticles");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.Articles");
        }
    }
}
