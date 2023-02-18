using Core.Entities;
using Data.Context;

namespace Data.Repositories
{
    internal class OrderWindowRepository : GenericRepository<OrderWindow>, IOrderWindowRepository
    {
        public OrderWindowRepository(DataContext context) : base(context)
        {
        }
    }
}
