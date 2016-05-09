using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace news.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MinLength(5)]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool Confirmed { get; set; }

        public virtual IList<Comment> Comments { get; set; }
        public virtual IList<Article> Articles { get; set; }
    }
}