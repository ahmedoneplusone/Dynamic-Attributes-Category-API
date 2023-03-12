using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Attribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Please enter valid value less than 100 characters")]
        public string Name { get; set; } = "";
        [ForeignKey("AttributeType")]
        public int TypeId { get; set; }
        public virtual AttributeType? AttributeType { get; set; }
        public virtual ICollection<CategoryAttributes>? categoryAttributes { get; set; }
    }
}
