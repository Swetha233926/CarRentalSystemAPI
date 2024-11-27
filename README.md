# CarRentalSystemAPI

The **CarRentalSystemAPI** is a RESTful API designed for managing a car rental service. It features user authentication, role-based access control, email notifications, and database management for cars, users, and transactions.

---

## Configuration

The application requires the following settings in the `appsettings.json` file:

```json
{
  "SendGrid": {
    "ApiKey": "Mysendgridkey",
    "FromEmail": "swethalakkavattula23@gmail.com",
    "FromName": "CarRentalSystem"
  },
  "JwtSettings": {
    "Key": "YourSuperSecretKeyThatIsLongEnough123",
    "Issuer": "CarRentalSystemAPI",
    "Audience": "CarRentalSystemAPIUsers",
    "ExpiresInMinutes": 60
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=SWETHA\\SQLEXPRESS01;Database=CarDatabase;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

end

