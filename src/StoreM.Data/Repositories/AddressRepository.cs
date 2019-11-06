using StoreM.Business.Interfaces;
using StoreM.Business.Models;
using StoreM.Data.Context;
using System;
using System.Threading.Tasks;

namespace StoreM.Data.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(StoreDbContext context) : base(context)
        {
        }

        public Task<Address> GetAddressByProvider(Guid providerId)
        {
            throw new NotImplementedException();
        }
    }
}
