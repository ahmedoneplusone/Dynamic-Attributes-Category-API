using Dynamic.Presentation.ActionFilters;
using Dynamic.Presentation.ModelBinders;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace Dynamic.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CategoryController(IServiceManager service)
        {
            _service = service;
        }

        [HttpOptions]
        public IActionResult GetCategoriesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, DELETE");

            return Ok();
        }

        [HttpGet(Name = "GetCategories")]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryParameters categoryParameters)
        {
            var pagedResult = await _service.CategoryService.GetAllCategoriesAsync(categoryParameters, trackChanges:false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.categories);
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id, [FromQuery] string[] includes)
        {
            var Category = await _service.CategoryService.GetCategoryByIdAsync(id, includes, trackChanges: false);
            return Ok(Category);
        }

        [HttpGet("collection/({ids})", Name = "GetCategoriesByIds")]
        public async Task<IActionResult> GetCategoriesByIds([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            var categories = await _service.CategoryService.GetCategoriesByIdsAsync(ids, trackChanges: false);
            return Ok(categories);
        }

        [HttpPost(Name = "CreateCategory")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDto category)
        {
            var Createdcategory = await _service.CategoryService.CreateCategoryAsync(category);
            return CreatedAtRoute(nameof(GetCategoryById), new { id = Createdcategory.Id }, Createdcategory);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCategories([FromBody] IEnumerable<CategoryForCreationDto> categories)
        {
            if (categories is null)
                return BadRequest("IEnumerable<CategoryForCreationDto> object is null");

            if (categories.Any(r => !ModelState.IsValid))
            {
                return UnprocessableEntity(ModelState);
            }

            var result = await _service.CategoryService.CreateCategoriesAsync(categories);

            return CreatedAtRoute(nameof(GetCategoriesByIds), new { result.ids }, result.categories);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _service.CategoryService.DeleteCategoryAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryForUpdateDto category)
        {
            await _service.CategoryService.UpdateCategoryAsync(id, category, trackChanges: true);
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateCategory(int id, [FromBody] JsonPatchDocument<CategoryForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object is null");

            var result = await _service.CategoryService.GetCategoryForPatchAsync(id, categoryTrackChanges: true);
            patchDoc.ApplyTo(result.categoryToPatch, ModelState);

            TryValidateModel(result.categoryToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.CategoryService.SaveChangesForPatchAsync(result.categoryToPatch, result.categoryEntity);

            return NoContent();
        }
    }
}