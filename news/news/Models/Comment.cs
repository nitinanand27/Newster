using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace news.Models
{
    public class Comment
    {

        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ArticleId { get; set; }
        [Required]
        public string Text { get; set; }

        public virtual User User { get; set; }
        public virtual Article Article { get; set; }
    }
}
