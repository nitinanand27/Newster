using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace news.Models
{
    public class User
    {

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual IList<Comment> Comments { get; set; }
        public virtual IList<Article> Articles { get; set; }
    }
}