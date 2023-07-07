using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name can't be empty")]
        public string? Name { get; set; }

        public virtual ICollection<Animal>? Animals { get; set; }

    }
}
