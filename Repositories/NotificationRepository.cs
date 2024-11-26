using CarRentalSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using CarRentalSystemAPI.Data;

namespace CarRentalSystemAPI.Repositories
{
    public interface INotificationRepository
    {
        public Task LogNotification(string toEmail, string subject, string body);
    }
    public class NotificationRepository:INotificationRepository
    {
        private readonly CarDbContext context;
        public NotificationRepository(CarDbContext context)
        {
            this.context = context;
        }

        public async Task LogNotification(string toEmail,string subject,string body)
        {
            var notification = new Notification
            {
                Email = toEmail,
                Subject = subject,
                Body = body,
                SentAt = DateTime.UtcNow
            };
            context.Notifications.Add(notification);
            await context.SaveChangesAsync();
        }
    }
}
