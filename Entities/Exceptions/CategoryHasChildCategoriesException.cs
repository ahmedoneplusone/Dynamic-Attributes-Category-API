using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CategoryHasChildCategoriesException : BadRequestException
    {
        public CategoryHasChildCategoriesException(int Id) : base($"Category With Id: {Id} Cannot Be Deleted Due To It Having Child Categories, Please Delete Them First Then Try Again.")
        {
        }
    }
}
