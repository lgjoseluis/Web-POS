using System.Linq.Expressions;
using WebPOS.Domain;
using WebPOS.Infrastructure.Commons.Foundation.Request;

namespace WebPOS.Infrastructure.Persistences.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<bool> AddAsync(T item);

        Task<bool> UpdateAsync(T item);

        Task<bool> DeleteAsync(int id);

        IQueryable<T> GenEntityQuery(Expression<Func<T, bool>>? filter = null);

        IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class;
    }
}
