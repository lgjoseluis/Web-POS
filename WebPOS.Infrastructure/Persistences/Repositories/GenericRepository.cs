using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using WebPOS.Domain;
using WebPOS.Domain.Entities;
using WebPOS.Infrastructure.Commons.Foundation.Request;
using WebPOS.Infrastructure.Entities;
using WebPOS.Infrastructure.Helpers;
using WebPOS.Infrastructure.Persistences.Contracts;
using WebPOS.Utilitties.Statics.Enums;

namespace WebPOS.Infrastructure.Persistences.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : BaseEntity
    {
        private readonly PosContext _posContext;
        private readonly DbSet<T> _entity;

        public GenericRepository(PosContext posContext)
        {
            _posContext = posContext;
            _entity = _posContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            List<T> items = await _entity
                .Where(
                    w => w.State.Equals((int)StatusType.ACTIVE) &&
                    w.AuditDeleteUser == null && 
                    w.AuditDeleteDate == null
                ).AsNoTracking()
                .ToListAsync();

            return items;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            T? item = await _entity
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return item;
        }

        public async Task<bool> AddAsync(T item)
        {
            item.AuditCreateUser = 1;
            item.AuditCreateDate = DateTime.Now;

            await _entity.AddAsync(item);

            int recordsAffected = await _posContext.SaveChangesAsync();

            return recordsAffected > 0;
        }


        public async Task<bool> UpdateAsync(T item)
        {
            item.AuditUpdateUser = 1;
            item.AuditUpdateDate = DateTime.Now;

            _entity.Update(item);

            _posContext.Entry(item).Property(x => x.AuditCreateUser).IsModified = false;
            _posContext.Entry(item).Property(x => x.AuditCreateDate).IsModified = false;

            int recordsAffected = await _posContext.SaveChangesAsync();

            return recordsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            T item = await _entity
                .AsNoTracking()
                .SingleAsync(x => x.Id == id);

            item.State = 0;
            item.AuditDeleteUser = 1;
            item.AuditDeleteDate = DateTime.Now;

            _entity.Update(item);

            int recordsAffected = await _posContext.SaveChangesAsync();

            return recordsAffected > 0;
        }

        public IQueryable<T> GenEntityQuery(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _entity;

            if(query is not null)
            {
                query = query.Where(filter);
            }

            return query;
        }     

        public IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.OrderType == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort} ascending");

            if (pagination)
                queryDto = queryDto.Paginate(request);

            return queryDto;
        }
    }
}
