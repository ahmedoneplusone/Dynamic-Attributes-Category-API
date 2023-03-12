using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICategoryRepository
    {
        Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParameters categoryParameters, bool trackChanges);
        Task<Category> GetCategoryByIdAsync(int categoryId, string[] includes, bool trackChanges);
        Task<IEnumerable<Category>> GetCategoriesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        Task<bool> HasChildCategory(int categoryId, bool trackChanges);
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
