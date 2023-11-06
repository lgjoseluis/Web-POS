using Microsoft.EntityFrameworkCore;
using WebPOS.Domain.Entities;
using WebPOS.Infrastructure.Commons.Foundation.Request;
using WebPOS.Infrastructure.Commons.Foundation.Response;
using WebPOS.Infrastructure.Entities;
using WebPOS.Infrastructure.Persistences.Contracts;
using WebPOS.Utilitties.Statics.Enums;
using WebPOS.Utilitties.Statics.Strings;

namespace WebPOS.Infrastructure.Persistences.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PosContext posContext):base(posContext) {        }

        public async Task<BaseEntityResponse<Category>> ListCategory(BaseFiltersRequest filters)
        {
            BaseEntityResponse<Category> response = new BaseEntityResponse<Category>();            

            IQueryable<Category> categories = this.GenEntityQuery(
                    w => w.AuditDeleteDate == null && w.AuditDeleteUser == null
                ).AsNoTracking();

            if (filters.TypeFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.TypeFilter) 
                {
                    case 1: 
                        categories = categories.Where(
                            x => x.Name!
                            .ToLower()
                            .Contains(filters.TextFilter.ToLower())
                        );
                        break;
                    case 2:
                        categories = categories.Where(
                            x => x.Description!
                            .ToLower()
                            .Contains(filters.TextFilter.ToLower())
                        );
                        break;
                }
            }

            if(filters.StateFilter is not null)
            {
                categories = categories.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                categories = categories.Where(
                        x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && 
                            x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1)
                );
            }

            if(filters.Sort is null)
            {
                filters.Sort = FieldNames.ID;
            }

            response.TotalRecords = await categories.CountAsync();
            response.Items = await Ordering(filters, categories, !filters.Download).ToListAsync();

            return response;
        }    
    }
}
