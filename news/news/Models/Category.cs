using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace news.Model
{
    public class Category
    {

        [Key, Column("Id")]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, ForeignKey("Creator")]
        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public virtual IList<Article> Articles { get; set; }
    }
}