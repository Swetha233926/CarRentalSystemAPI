using CarRentalSystemAPI.Repositories;

namespace CarRentalSystemAPI.Services
{
    public interface ITransactionLogService
    {
        Task LogAsync(string action, string details, string user);
    }
    public class TransactionLogService:ITransactionLogService
    {
        private readonly ITransactionLogRepository _transactionLogRepository;

        public TransactionLogService(ITransactionLogRepository transactionLogRepository)
        {
            _transactionLogRepository = transactionLogRepository;
        }

        public async Task LogAsync(string action, string details, string user)
        {
            await _transactionLogRepository.LogTransaction(action, details, user);
        }
    }
}
