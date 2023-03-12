using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Shared.RequestFeatures
{
    public class CategoryParameters : RequestParameters
    {
        public CategoryParameters() {
            OrderBy = "name";
            SearchTerm = "";
        }

    }
}
