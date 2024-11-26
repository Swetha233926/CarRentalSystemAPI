using Microsoft.EntityFrameworkCore;
using CarRentalSystemAPI.Models;
namespace CarRentalSystemAPI.Data
{
    public class CarDbContext:DbContext
    {
        //constructor 
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }
        public DbSet<Car> cars { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<TransactionLog> TransactionLogs { get; set; }
    }
}
