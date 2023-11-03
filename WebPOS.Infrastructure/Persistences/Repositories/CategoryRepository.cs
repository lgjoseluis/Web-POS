using Microsoft.EntityFrameworkCore;
using WebPOS.Domain.Entities;
using WebPOS.Infrastructure.Commons.Foundation.Request;
using WebPOS.Infrastructure.Commons.Foundation.Response;
using WebPOS.Infrastructure.Entities;
using WebPOS.Infrastructure.Persistences.Contracts;
using WebPOS.Utilitties.Statics.Enums;

namespace WebPOS.Infrastructure.Persistences.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly PosContext _posContext;

        public CategoryRepository(PosContext posContext)
        {
            _posContext = posContext;
        }

        public async Task<BaseEntityResponse<Category>> ListCategory(BaseFiltersRequest filters)
        {
            BaseEntityResponse<Category> response = new BaseEntityResponse<Category>();

            IQueryable<Category> categories = (
                              from c in _posContext.Categories
                                where c.AuditDeleteUser == null && c.AuditDeleteDate == null
                                select c
                              )
                              .AsNoTracking()
                              .AsQueryable();

            if(filters.TypeFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.TypeFilter) 
                {
                    case 1: 
                        categories.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        categories.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                }
            }

            if(filters.StateFilter is not null)
            {
                categories.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                categories.Where(
                        x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && 
                            x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1)
                );
            }

            if(filters.Sort is null)
            {
                filters.Sort = "CategoryId";
            }

            response.TotalRecords = await categories.CountAsync();
            response.Items = await Ordering(filters, categories, !filters.Download).ToListAsync();

            return response;
        }

        public async Task<IEnumerable<Category>> ListSelectCategory()
        {
            IEnumerable<Category> categories = await _posContext.Categories
                .Where(
                    x => x.State.Equals(StatusType.ACTIVE) &&
                        x.AuditDeleteDate == null &&
                        x.AuditDeleteUser == null
                ).AsNoTracking()
                .ToListAsync();

            return categories;
        }

        public async Task<Category?> CategoryById(int categoryId)
        {
            Category? category = await _posContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CategoryId == categoryId);

            return category;
        }

        public async Task<bool> AddCategory(Category category)
        {
            category.AuditCreateUser = 1;
            category.AuditCreateDate = DateTime.Now;

            await _posContext.Categories.AddAsync(category);

            int recordsAffected = await _posContext.SaveChangesAsync();

            return recordsAffected > 0;
        }        

        public async Task<bool> UpdateCategory(Category category)
        {
            category.AuditUpdateUser = 1;
            category.AuditUpdateDate = DateTime.Now;

            _posContext.Categories.Update(category);

            _posContext.Entry(category).Property(x => x.AuditCreateUser).IsModified = false;
            _posContext.Entry(category).Property(x => x.AuditCreateDate).IsModified = false;

            int recordsAffected = await _posContext.SaveChangesAsync();

            return recordsAffected > 0;
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            Category category = await _posContext.Categories
                .AsNoTracking()
                .SingleAsync(x => x.CategoryId == categoryId);

            category.AuditDeleteUser = 1;
            category.AuditDeleteDate = DateTime.Now;

            int recordsAffected = await _posContext.SaveChangesAsync();

            return recordsAffected > 0;
        }
    }
}
