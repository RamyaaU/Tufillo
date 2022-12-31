using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tufillo.Infrastructure.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1,int.MaxValue)]
        public double Price { get; set; }

        public string Image { get; set; }

        //to configure FK relation to CategoryId, create a property 
        //by this EF automatically adds a mapping between category and product
        //for EF to make it aware regarding the FK relationship, dataannotation must be added
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        //FK data column 
        [Display(Name="Category Type")]
        public int CategoryId { get; set; }
    }
}
