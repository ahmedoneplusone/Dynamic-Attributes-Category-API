using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Interfaces
{
    public interface ICategoryService
    {
        Task<(IEnumerable<CategoryDto> categories, MetaData metaData)> GetAllCategoriesAsync(CategoryParameters categoryParameters, bool trackChanges);
        Task<CategoryDto> GetCategoryByIdAsync(int categoryId, string[] includes, bool trackChanges);
        Task<IEnumerable<CategoryDto>> GetCategoriesByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto company);
        Task<(IEnumerable<CategoryDto> categories, string ids)> CreateCategoriesAsync(IEnumerable<CategoryForCreationDto> categories);
        Task DeleteCategoryAsync(int categoryId, bool trackChanges);
        Task UpdateCategoryAsync(int categoryId, CategoryForUpdateDto categoryForUpdate, bool trackChanges);
        Task<(CategoryForUpdateDto categoryToPatch, Category categoryEntity)> GetCategoryForPatchAsync(int categoryId, bool categoryTrackChanges);
        Task SaveChangesForPatchAsync(CategoryForUpdateDto categoryToPatch, Category categoryEntity);

    }
}