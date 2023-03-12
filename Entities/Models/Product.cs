using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Please enter valid value less than 100 characters")]
        public string Name { get; set; }
        [Required]
        [Range(float.MinValue, float.MaxValue, ErrorMessage = "Please enter valid price value")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public float Price { get; set; }
        [Required]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Please enter valid integer value")]
        public int Quantity { get; set; }
        public virtual ICollection<CategoryProduct>? CategoryProducts { get; set; }
    }
}
