using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record CategoryForManipulationDto
    {
        [Required(ErrorMessage = "Category name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the name is 30 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Category Parent ID is a required field.")]
        public int? ParentCategoryId { get; init; }

    }
}
