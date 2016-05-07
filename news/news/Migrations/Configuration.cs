namespace news.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using news.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<news.Models.NewsterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(news.Models.NewsterContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if (context.Categories.Count() == 0)
            {

                context.Categories.Add(new Category { Name = "sports" });
                context.Categories.Add(new Category { Name = "catpix" });
                context.Categories.Add(new Category { Name = "breakingnews" });
                context.Categories.Add(new Category { Name = "random" });
                context.Categories.Add(new Category { Name = "funny" });

                context.SaveChanges();
            }
        }
    }
}
