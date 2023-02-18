using Core.Entities;
using Data.Context;

namespace Data.Repositories
{
    internal class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository(DataContext context) : base(context)
        {
        }
    }
}
