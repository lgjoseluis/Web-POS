using WebPOS.Application.Commons.Foundations;
using WebPOS.Application.Dtos.Request;
using WebPOS.Application.Dtos.Response;
using WebPOS.Infrastructure.Commons.Foundation.Request;
using WebPOS.Infrastructure.Commons.Foundation.Response;

namespace WebPOS.Application.Contracts
{
    public interface ICategoryApplication
    {
        Task<BaseResponse<BaseEntityResponse<CategoryResponseDto>>> ListCategories(BaseFiltersRequest filters);

        Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories();

        Task<BaseResponse<CategoryResponseDto>> GetCategoryById(int categoryId);

        Task<BaseResponse<bool>> AddCategory(CategoryRequestDto request);

        Task<BaseResponse<bool>> UpdateCategory(int categoryId, CategoryRequestDto request);

        Task<BaseResponse<bool>> DeleteCategory(int categoryId);
    }
}
