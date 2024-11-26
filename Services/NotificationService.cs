using CarRentalSystemAPI.Models;
using CarRentalSystemAPI.Repositories;

namespace CarRentalSystemAPI.Services
{
    public interface INotificationService
    {
        public Task SendNotification(NotificationDto notificationDto);
    }
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IConfiguration _configuration;
        private readonly SendGrid.SendGridClient _sendGridClient;

        public NotificationService(INotificationRepository notificationRepository, IConfiguration configuration)
        {
            _notificationRepository = notificationRepository;
            _configuration = configuration;
            //_sendGridClient = new SendGrid.SendGridClient(_configuration["SendGrid:ApiKey"]);
        }

        public async Task SendNotification(NotificationDto notificationDto)
        {
            var fromEmail = _configuration["SendGrid:FromEmail"];
            var fromName = _configuration["SendGrid:FromName"];

            var message = new SendGrid.Helpers.Mail.SendGridMessage
            {
                From = new SendGrid.Helpers.Mail.EmailAddress(fromEmail, fromName),
                Subject = notificationDto.Subject,
                PlainTextContent = notificationDto.Body,
                HtmlContent = notificationDto.Body // You can also use HTML here
            };

            message.AddTo(notificationDto.ToEmail);

            // Send the email using SendGrid
            /*var response = await _sendGridClient.SendEmailAsync(message);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email. Status Code: {response.StatusCode}");
            }
            */

            // Log the notification to the database (optional)
            await _notificationRepository.LogNotification(notificationDto.ToEmail, notificationDto.Subject, notificationDto.Body);
        }
    }
}
