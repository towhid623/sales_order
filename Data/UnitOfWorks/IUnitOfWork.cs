using Data.Repositories;

namespace Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public ICustomerRepository CustomerRepository { get; }
        public IStateRepository StateRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IWindowRepository WindowRepository { get; }
        public IOrderWindowRepository OrderWindowRepository { get; }
        public IOrderWindowElementRepository OrderWindowElementRepository { get; }
        Task CompleteAsync();
        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
