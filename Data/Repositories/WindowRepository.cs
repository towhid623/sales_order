using Core.Entities;
using Data.Context;

namespace Data.Repositories
{
    internal class WindowRepository : GenericRepository<Window>, IWindowRepository
    {
        public WindowRepository(DataContext context) : base(context)
        {
        }
    }
}
