 using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tufillo.Infrastructure.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //displayname is one kind of data annotation that is used to display your
        //model in ui whichever way you want
        [DisplayName("Display Order")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Display order for category must be greater than 0")]
        public int DisplayOrder { get; set; }
    }
}
