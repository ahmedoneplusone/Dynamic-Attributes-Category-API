using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Please enter valid value less than 100 characters")]
        public string Name { get; set; } = "";
        public int? ParentCategoryId { get; set; }

        [InverseProperty("ChildrenCategories")]
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category>? ChildrenCategories { get; set; }
        public virtual ICollection<CategoryAttributes>? CategoryAttributes { get; set; }
        public virtual ICollection<CategoryProduct>? CategoryProducts { get; set; }

    }
}
