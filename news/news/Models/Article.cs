using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace news.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required, MaxLength(140)]
        public string Text { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string SourceAdress { get; set; }

        public string ImgAdress { get; set; }

        public string VideoAdress { get; set; }

        /// <summary>
        /// Foreign keys
        /// </summary>
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual IList<Comment> Comments { get; set; }
    }
}