using WebPOS.Infrastructure.Commons.Foundation.Request;

namespace WebPOS.Infrastructure.Helpers
{
    public static class QueryableHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, BasePaginationRequest pagination)
        {
            return queryable
                .Skip((pagination.PageNumber-1) * pagination.Records)
                .Take(pagination.Records);
        }
    }
}
