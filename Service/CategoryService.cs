using AutoMapper;
using Interfaces;
using Entities.Exceptions;
using Entities.Models;
using LoggerService;
using Service.Interfaces;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CategoryService(IRepositoryManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<CategoryDto> categories, MetaData metaData)> GetAllCategoriesAsync(CategoryParameters categoryParameters, bool trackChanges)
        {
            var categoriesWithMetaData = await _repo.Category.GetAllCategoriesAsync(categoryParameters, trackChanges);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categoriesWithMetaData);

            return (categories: categoriesDto, metaData: categoriesWithMetaData.MetaData);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId, string includes, bool trackChanges)
        {
            var category = await GetCategoryAndCheckIfItExists(categoryId, includes, trackChanges);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesByIdsAsync(IEnumerable<int> ids, string includes, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var categoryEntities = await _repo.Category.GetCategoriesByIdsAsync(ids, includes, trackChanges);

            if (ids.Count() != categoryEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var categoriesToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);

            return categoriesToReturn;
        }


        public async Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);

            _repo.Category.CreateCategory(categoryEntity);
            await _repo.SaveAsync();

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);
            return categoryToReturn;
        }
        public async Task<(IEnumerable<CategoryDto> categories, string ids)> CreateCategoriesAsync(IEnumerable<CategoryForCreationDto> categories)
        {
            if (categories is null)
                throw new CategoriesBadRequest();

            var categoryEntities = _mapper.Map<IEnumerable<Category>>(categories);
            foreach (var category in categoryEntities)
                _repo.Category.CreateCategory(category);

            await _repo.SaveAsync();

            var categoriesToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);
            var ids = string.Join(",", categoriesToReturn.Select(c => c.Id));

            return (categories: categoriesToReturn, ids: ids);
        }

        public async Task DeleteCategoryAsync(int categoryId, bool trackChanges)
        {
            var category = await GetCategoryAndCheckIfItExists(categoryId, includes: "", trackChanges);

            var hasChildrenCategory = await _repo.Category.HasChildCategory(categoryId, trackChanges);
            if (hasChildrenCategory == null || hasChildrenCategory)
                throw new CategoryHasChildCategoriesException(categoryId);

            _repo.Category.DeleteCategory(category);
            await _repo.SaveAsync();
        }

        public async Task UpdateCategoryAsync(int categoryId, CategoryForUpdateDto categoryForUpdate, bool trackChanges)
        {
            var categoryEntity = await GetCategoryAndCheckIfItExists(categoryId, includes: "", trackChanges);

            _mapper.Map(categoryForUpdate, categoryEntity);
            await _repo.SaveAsync();
        }

        public async Task<(CategoryForUpdateDto categoryToPatch, Category categoryEntity)> GetCategoryForPatchAsync(int categoryId, bool categoryTrackChanges)
        {
            var categoryEntity = await _repo.Category.GetCategoryByIdAsync(categoryId, includes: "", categoryTrackChanges);
            if (categoryEntity is null)
                throw new CategoryNotFoundException(categoryId);

            var categoryToPatch = _mapper.Map<CategoryForUpdateDto>(categoryEntity);
            return (categoryToPatch, categoryEntity);
        }

        public async Task SaveChangesForPatchAsync(CategoryForUpdateDto categoryToPatch, Category categoryEntity)
        {
            _mapper.Map(categoryToPatch, categoryEntity);
            await _repo.SaveAsync();
        }

        private async Task<Category> GetCategoryAndCheckIfItExists(int id, string includes, bool trackChanges) { 
            var category = await _repo.Category.GetCategoryByIdAsync(id, includes, trackChanges);
            if (category is null)
                throw new CategoryNotFoundException(id);

            return category; 
        }
    }
}