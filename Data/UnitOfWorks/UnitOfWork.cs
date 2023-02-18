using Data.Context;
using Data.Repositories;

namespace Data.UnitOfWorks
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private bool _disposed;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        private CustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_dataContext);

        private StateRepository _stateRepository;
        public IStateRepository StateRepository => _stateRepository ??= new StateRepository(_dataContext);

        private OrderRepository _orderRepository;
        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_dataContext);

        private WindowRepository _windowRepository;
        public IWindowRepository WindowRepository => _windowRepository ??= new WindowRepository(_dataContext);

        private OrderWindowRepository _orderWindowRepository;
        public IOrderWindowRepository OrderWindowRepository => _orderWindowRepository ??= new OrderWindowRepository(_dataContext);

        private OrderWindowElementRepository _orderWindowElementRepository;
        public IOrderWindowElementRepository OrderWindowElementRepository => _orderWindowElementRepository ??= new OrderWindowElementRepository(_dataContext);

        /// <summary>
        /// Completes the unit of work, saving all repository changes to the underlying data-store.
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task CompleteAsync() => await _dataContext.SaveChangesAsync();

        public async Task BeginAsync() => await _dataContext.Database.BeginTransactionAsync();

        public async Task CommitAsync() => await _dataContext.Database.CommitTransactionAsync();

        public async Task RollbackAsync() => await _dataContext.Database.RollbackTransactionAsync();

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        /// <returns><see cref="ValueTask"/></returns>
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);

            // Take this object off the finalization queue to prevent 
            // finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        /// <param name="disposing">Whether or not we are disposing</param> 
        /// <returns><see cref="ValueTask"/></returns>
        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    await _dataContext.DisposeAsync();
                }

                // Dispose any unmanaged resources here...

                _disposed = true;
            }
        }
    }
}
