using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderWindow> OrderWindows { get; set; }
        public DbSet<OrderWindowElement> OrderWindowElements { get; set; }
    }
}
