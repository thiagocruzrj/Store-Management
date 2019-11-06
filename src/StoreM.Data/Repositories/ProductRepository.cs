using Microsoft.EntityFrameworkCore;
using StoreM.Business.Interfaces;
using StoreM.Business.Models;
using StoreM.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreM.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(StoreDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Product>> GetProductsByProvider(Guid providerId)
        {
            return await Search(p => p.ProviderId == providerId);
        }

        public async Task<IEnumerable<Product>> GetProductsProviders()
        {
            return await Db.Products.AsNoTracking()
                .Include(f => f.Provider)
                .OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Product> GetProdutProvider(Guid id)
        {
            return await Db.Products.AsNoTracking()
                .Include(f => f.Provider)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
