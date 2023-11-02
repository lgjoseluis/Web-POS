using System.Linq.Dynamic.Core;
using WebPOS.Infrastructure.Commons.Foundation.Request;
using WebPOS.Infrastructure.Helpers;
using WebPOS.Infrastructure.Persistences.Contracts;

namespace WebPOS.Infrastructure.Persistences.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        protected IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.OrderType == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort} ascending");

            if (pagination)
                queryDto = queryDto.Paginate(request);

            return queryDto;
        }
    }
}
