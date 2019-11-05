using StoreM.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreM.Business.Interfaces
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider> GetProviderByAddres(Guid id);
        Task<Provider> GetProviderProductsAddress(Guid id);
    }
}
