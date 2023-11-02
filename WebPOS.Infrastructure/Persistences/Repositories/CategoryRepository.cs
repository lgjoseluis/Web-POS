using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPOS.Domain.Entities;
using WebPOS.Infrastructure.Entities;
using WebPOS.Infrastructure.Persistences.Contracts;

namespace WebPOS.Infrastructure.Persistences.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly PosContext _posContext;

        public CategoryRepository(PosContext posContext)
        {
            _posContext = posContext;
        }
    }
}
