using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace news.Model
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class NewsterContext : DbContext
    {

        public NewsterContext() : base("mySqlCon") { }

        public DbSet<User> Users
        {
            get; set;
        }
        public DbSet<Article> Articles
        {
            get; set;
        }

        public DbSet<Category> Categories { get; set; }
    }
}
