using Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Extensions;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category> , ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Category>> GetAllCategoriesAsync(CategoryParameters categoryParameters, bool trackChanges)
        {
            var Categories = await FindAll(trackChanges)
                .Search(categoryParameters.SearchTerm != null ? categoryParameters.SearchTerm : "")
                .Sort(categoryParameters.OrderBy != null ? categoryParameters.OrderBy : "")
                .Skip((categoryParameters.PageNumber - 1) * categoryParameters.PageSize)
                .Take(categoryParameters.PageSize)
                .CustomInclude(categoryParameters.Includes != null ? categoryParameters.Includes : "")
                .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<Category>(Categories, count, categoryParameters.PageNumber, categoryParameters.PageSize);
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId, string[] includes, bool trackChanges)
        {
            var query = FindByCondition(c => c.Id.Equals(categoryId), trackChanges).AsQueryable();
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
            return await query.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByIdsAsync(IEnumerable<int> ids, bool trackChanges) => 
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).OrderBy(c => c.Name).ToListAsync();

        public async Task<bool> HasChildCategory(int categoryId, bool trackChanges) =>
            await FindByCondition(x => x.ParentCategoryId.Equals(categoryId),trackChanges).CountAsync() > 0 ? true : false;
        public void CreateCategory(Category category) => Create(category);
       
        public void DeleteCategory(Category category) => Delete(category);


    }
}
