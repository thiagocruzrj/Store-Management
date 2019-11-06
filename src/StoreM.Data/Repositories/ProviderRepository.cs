using Microsoft.EntityFrameworkCore;
using StoreM.Business.Interfaces;
using StoreM.Business.Models;
using StoreM.Data.Context;
using System;
using System.Threading.Tasks;

namespace StoreM.Data.Repositories
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<Provider> GetProviderByAddres(Guid id)
        {
            return await Db.Providers.AsNoTracking()
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Provider> GetProviderProductsAddress(Guid id)
        {
            return await Db.Providers.AsNoTracking()
                .Include(c => c.Products)
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
