using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace news.Models
{
    public class Category
    {

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public virtual IList<Article> Articles { get; set; }
    }
}