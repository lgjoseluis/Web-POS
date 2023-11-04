using Microsoft.AspNetCore.Mvc;
using WebPOS.Application.Commons.Foundations;
using WebPOS.Application.Contracts;
using WebPOS.Application.Dtos.Request;
using WebPOS.Application.Dtos.Response;
using WebPOS.Infrastructure.Commons.Foundation.Request;
using WebPOS.Infrastructure.Commons.Foundation.Response;

namespace WebPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;

        public CategoryController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> ListCategories([FromBody] BaseFiltersRequest filters)
        {
            BaseResponse<BaseEntityResponse<CategoryResponseDto>> response = await _categoryApplication.ListCategories(filters);

            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectCategories()
        {
            BaseResponse<IEnumerable<CategorySelectResponseDto>> resopnse =await _categoryApplication.ListSelectCategories();

            return Ok(resopnse);
        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            BaseResponse<CategoryResponseDto> response = await _categoryApplication.GetCategoryById(categoryId);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDto request)
        {
            BaseResponse<bool> response = await _categoryApplication.AddCategory(request);

            return Ok(response);
        }

        [HttpPut("{categoryId:int}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryRequestDto request)
        {
            BaseResponse<bool> response = await _categoryApplication.UpdateCategory(categoryId, request);

            return Ok(response);
        }

        [HttpDelete("{categoryId:int}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            BaseResponse<bool> response = await _categoryApplication.DeleteCategory(categoryId);

            return Ok(response);
        }
    }
}
