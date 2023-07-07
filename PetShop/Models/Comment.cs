using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace PetShop.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment can't be empty")]
        public string? Content { get; set; } = null;

        [Required(ErrorMessage = "Comment must belong to a selected animal")]
        public int AnimalId { get; set; }

        public virtual Animal? Animal { get; set; }
    }
}
