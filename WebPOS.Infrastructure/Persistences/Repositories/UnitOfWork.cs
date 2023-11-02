using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPOS.Infrastructure.Entities;
using WebPOS.Infrastructure.Persistences.Contracts;

namespace WebPOS.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PosContext _posContext;
        public ICategoryRepository CategoryRepository { get; private set;}

        public UnitOfWork(PosContext context)
        {
            _posContext = context;
            CategoryRepository = new CategoryRepository(_posContext);
        }

        public void Dispose()
        {
            _posContext.Dispose();
        }

        public void SaveChanges()
        {
            _posContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _posContext.SaveChangesAsync();
        }
    }
}
