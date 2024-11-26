using CarRentalSystemAPI.Repositories;
using CarRentalSystemAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CarRentalSystemAPI.Services
{
    public interface ICarRentalService
    {
        Task<(bool Value, decimal PricePerDay)> RentCar(int carId, int userId, int rentalDays);
        Task<bool> CheckCarAvailability(int carId);

        Task<IEnumerable<Car>> GetAllCars();
        Task<Car> GetCarById(int carId);
        Task AddCar(Car car);
        Task UpdateCarDetails(Car car);
        Task UpdateCarAvailability(Car car);
        Task DeleteCar(int carId);
    }

    public class CarRentalService : ICarRentalService
    {
        private readonly ICarRepository carRepository;
        private readonly INotificationService notificationService;
        private readonly ITransactionLogService transactionLogService;
        private readonly IUserRepository userRepository;

        public CarRentalService(
            ICarRepository carRepository,
            INotificationService notificationService,
            ITransactionLogService transactionLogService,
            IUserRepository userRepository)
        {
            this.carRepository = carRepository;
            this.notificationService = notificationService;
            this.transactionLogService = transactionLogService;
            this.userRepository = userRepository;
        }

        // Method to rent a car
        public async Task<(bool Value, decimal PricePerDay)> RentCar(int carId, int userId, int rentalDays)
        {
            var car = await carRepository.GetCarById(carId);
            if (car == null)
            {
                await transactionLogService.LogAsync("Car Rental Failed", $"CarId: Car with the car id {carId} does not exist.", $"UserId: {userId}");
                return (false, 0); // Car does not exist
            }
            if (!car.IsAvailable)
            {
                await transactionLogService.LogAsync("Car Rental Failed", $"Car with id {carId} is unavailable at this moment.", $"UserId: {userId}");
                return (false, -1); // Car is unavailable
            }

            car.IsAvailable = false;
            await carRepository.UpdateCarAvailability(car);

            // Retrieve user details from the userId (example, you would replace this with actual logic to fetch user data)
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                await transactionLogService.LogAsync("Car Rental Failed", $"UserId: User with id {userId} does not exist.", $"CarId: {carId}");
                return (false, 0); // User does not exist
            }

            // Send Notification
            var notificationDto = new NotificationDto
            {
                ToEmail = user.User_email, // Use the user's email
                Subject = "Car Rental Confirmation",
                Body = $@"
            Hello {user.User_name}, 

            Your car rental is confirmed.

            Car Details:
            - Make: {car.Make}
            - Model: {car.Model}
            - Rental Duration: {rentalDays} days

            Thank you for choosing our service!"
            };
            await notificationService.SendNotification(notificationDto);

            // Log Transaction
            var transactionDetails = $"Car rented: {car.Make} {car.Model}, Rental Duration: {rentalDays} days";
            await transactionLogService.LogAsync("Car Rental Success", transactionDetails, $"UserId: {userId}");

            return (true, car.PricePerDay); // Car rented successfully, return PricePerDay for further calculations
        }


        // Method to check car availability
        public async Task<bool> CheckCarAvailability(int carId)
        {
            var car = await carRepository.GetCarById(carId);
            return car != null && car.IsAvailable;
        }

        // Getting all cars
        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await carRepository.GetAllCars();
        }

        // Getting a car by ID
        public async Task<Car> GetCarById(int carId)
        {
            return await carRepository.GetCarById(carId);
        }

        // Adding a car
        public async Task AddCar(Car car)
        {
            await carRepository.AddCar(car);

            // Log Transaction
            var transactionDetails = $"Car added: {car.Make} {car.Model}";
            await transactionLogService.LogAsync("Car Added", transactionDetails, "Admin");
        }

        // Updates the car’s details (Make, Model, Price, etc.)
        public async Task UpdateCarDetails(Car car)
        {
            await carRepository.UpdateCarDetails(car);

            // Log Transaction
            var transactionDetails = $"Car updated: {car.Make} {car.Model}";
            await transactionLogService.LogAsync("Car Updated", transactionDetails, "Admin");
        }

        // Update only the availability status of the car
        public async Task UpdateCarAvailability(Car car)
        {
            await carRepository.UpdateCarAvailability(car);

            // Log Transaction
            var transactionDetails = $"Car availability updated: {car.Make} {car.Model}";
            await transactionLogService.LogAsync("Car Availability Updated", transactionDetails, "Admin");
        }

        // Delete the car
        public async Task DeleteCar(int id)
        {
            var car = await carRepository.GetCarById(id);
            if (car != null)
            {
                await carRepository.DeleteCar(id);

                // Log Transaction
                var transactionDetails = $"Car deleted: {car.Make} {car.Model}";
                await transactionLogService.LogAsync("Car Deleted", transactionDetails, "Admin");
            }
        }
    }
}
