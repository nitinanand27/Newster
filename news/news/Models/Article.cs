using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace news.Model
{
    public class Article
    {

        [Key, Column("Id")]
        public int ArticleId { get; set; }

        [ForeignKey("User")]
        public int Author { get; set; }

        [Required]
        public string Heading { get; set; }

        [Required, MaxLength(140)]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, Column("Source")]
        public string SourceAdress { get; set; }

        public string ImgAdress { get; set; }

        public string VideoAdress { get; set; }

        /// <summary>
        /// Foreign keys
        /// </summary>
        public virtual User User { get; set; }
        public virtual IList<Category> Categories { get; set; }
    }
}