using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace news.Models
{
    public class Article
    {

        public int ArticleId { get; set; }

        public string Heading { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

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