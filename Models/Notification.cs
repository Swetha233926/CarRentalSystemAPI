namespace CarRentalSystemAPI.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentAt { get; set; }
    }
}
