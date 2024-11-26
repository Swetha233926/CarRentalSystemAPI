using CarRentalSystemAPI.Data;
using CarRentalSystemAPI.Models;

namespace CarRentalSystemAPI.Repositories
{
    public interface ITransactionLogRepository
    {
        public Task LogTransaction(string action, string details, string user);
    }
    public class TransactionLogRepository:ITransactionLogRepository
    {
        private readonly CarDbContext _context;

        public TransactionLogRepository(CarDbContext context)
        {
            _context = context;
        }

        public async Task LogTransaction(string action, string details, string user)
        {
            var log = new TransactionLog
            {
                Action = action,
                Details = details,
                User = user,
                Timestamp = DateTime.UtcNow
            };

            _context.TransactionLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
