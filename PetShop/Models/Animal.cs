using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name can't be empty")]
        public string? Name { get; set; } = null;

        [Range(1,300)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Description can't be empty")]
        [StringLength(500, ErrorMessage = "500 char limit reached")]
        public string? Description { get; set; } = null;

        public string? PictureName { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "You must upload a picture")]
        public IFormFile? Picture { get; set; }
    }
}
