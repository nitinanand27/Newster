using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace news.Models
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class NewsterContext : DbContext
    {

        public NewsterContext() : base("mySqlCon") { }

        public DbSet<User> Users{ get; set; }
        public DbSet<Article> Articles{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Konfigurera modellklasserna här

            var articleEntity = modelBuilder.Entity<Article>();
            var userEntity = modelBuilder.Entity<User>();
            var categoryEntity = modelBuilder.Entity<Category>();
            var commentEntity = modelBuilder.Entity<Comment>();

            articleEntity.ToTable("Articles");
            userEntity.ToTable("Users");
            categoryEntity.ToTable("Categories");
            commentEntity.ToTable("Comments");

            articleEntity.HasMany(c=>c.Comments).WithRequired(x=>x.Article);
            articleEntity.HasRequired(u => u.User).WithMany(a => a.Articles);
            categoryEntity.HasMany(c => c.Articles).WithRequired(a => a.Category);
            userEntity.HasMany(u => u.Comments).WithRequired(c => c.User);
            
            //modelBuilder.Entity<Article>()
            //    .HasMany<Comment>

            base.OnModelCreating(modelBuilder);
        }

    }
}
