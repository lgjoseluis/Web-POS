using WebPOS.Domain.Entities;
using WebPOS.Infrastructure.Commons.Foundation.Request;
using WebPOS.Infrastructure.Commons.Foundation.Response;

namespace WebPOS.Infrastructure.Persistences.Contracts
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<BaseEntityResponse<Category>> ListCategory(BaseFiltersRequest filters);
    }
}
