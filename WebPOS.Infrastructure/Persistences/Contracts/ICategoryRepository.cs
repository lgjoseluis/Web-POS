using WebPOS.Domain.Entities;
using WebPOS.Infrastructure.Commons.Foundation.Request;
using WebPOS.Infrastructure.Commons.Foundation.Response;

namespace WebPOS.Infrastructure.Persistences.Contracts
{
    public interface ICategoryRepository
    {
        Task<BaseEntityResponse<Category>> ListCategory(BaseFiltersRequest filters);

        Task<IEnumerable<Category>> ListSelectCategory();

        Task<Category?> CategoryById(int categoryId);

        Task<bool> AddCategory(Category category);

        Task<bool> UpdateCategory(Category category); 

        Task<bool> DeleteCategory(int categoryId);
    }
}
