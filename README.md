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

Endpoints
Authentication Endpoints
Register: Enables user and admin registration.
Login: Provides JWT tokens for authentication.
Car Management Endpoints
Get All Cars: Retrieves all cars.
Get Car by ID: Fetches details for a specific car.
Check Car Availability: Checks if a car is available for rental.
Add a Car: Admins can add new cars to the inventory.
Update a Car: Admins can update car details.
Delete a Car: Admins can remove cars from the inventory.
Rent a Car: Users can rent cars for a specific duration.
Transaction Log Endpoints
Logs user and admin actions, such as adding, updating, or renting cars.

User Management Endpoints
Get User by ID: Retrieves user details by ID.
Get User by Email: Fetches user data using an email address.
Delete User: Allows admins to delete users.
Project Highlights
JWT Authentication: Secures endpoints based on roles (Admin and User).
SendGrid Integration: Sends email notifications to users upon successful rentals.
SQL Server Database: Manages data for users, cars, and transactions.
Role-Based Access Control:
Admin: Manages users and cars.
User: Rents cars and views availability.
Setup Instructions
Clone the Repository:

bash
Copy code
git clone https://github.com/Swetha233926/CarRentalSystemAPI.git
cd CarRentalSystemAPI
Configure appsettings.json:

Ensure appsettings.json is in the root directory.
Update SendGrid:ApiKey, JwtSettings:Key, and ConnectionStrings:DefaultConnection with your configurations.
Run the Application:

Use Visual Studio or .NET CLI to run the project:
bash
Copy code
dotnet run
Test the API:

Use Postman or any API testing tool to verify the functionality of endpoints.
API Testing
The API has been tested using Postman. Example scenarios include:

User login and authentication with valid/invalid credentials.
Admin actions like adding, updating, or deleting cars.
Renting cars with user roles.
Handling error cases, such as unavailable cars.
Notes
Secure API Keys: Sensitive data like SendGrid:ApiKey should not be hardcoded in appsettings.json for production. Use environment variables or secrets management tools.
Database Setup: Ensure SQL Server is installed and the CarDatabase schema is properly configured.

