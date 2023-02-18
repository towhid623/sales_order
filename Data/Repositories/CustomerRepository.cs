using Core.Entities;
using Data.Context;

namespace Data.Repositories
{
    internal class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }
    }
}
