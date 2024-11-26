namespace CarRentalSystemAPI.Models
{
    public class TransactionLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
        public string User {  get; set; }
    }
}
