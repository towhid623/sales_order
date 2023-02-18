using Core.Entities;
using Data.Context;

namespace Data.Repositories
{
    internal class OrderWindowElementRepository : GenericRepository<OrderWindowElement>, IOrderWindowElementRepository
    {
        public OrderWindowElementRepository(DataContext context) : base(context)
        {
        }
    }
}
