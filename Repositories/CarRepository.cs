using CarRentalSystemAPI.Data;
using CarRentalSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace CarRentalSystemAPI.Repositories
{
    public interface ICarRepository
    {
        public Task AddCar(Car car);
        public Task UpdateCarAvailability(Car car);
        public Task<IEnumerable<Car>> GetAllCars();
        public Task<Car> GetCarById(int id);
        public Task UpdateCarDetails(Car car);
        public Task DeleteCar(int id);
  
    }
    
    public class CarRepository: ICarRepository
    {
        private readonly CarDbContext context;

        public CarRepository(CarDbContext context)
        {
            this.context = context;
        }

        // Adding the car details asynchronously
        public async Task AddCar(Car car)
        {
            await context.cars.AddAsync(car); 
            await context.SaveChangesAsync(); 
        }

        // Updating the car details asynchronously
        public async Task UpdateCarAvailability(Car car)
        {
            var existingCar = await context.cars.FindAsync(car.Id);  
            if (existingCar != null)
            {
                existingCar.IsAvailable = car.IsAvailable;  
                await context.SaveChangesAsync();
            }
        }

        // Get all cars asynchronously
        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await context.cars.ToListAsync(); 
        }

        // Get car by ID asynchronously
        public async Task<Car> GetCarById(int id)
        {
            return await context.cars.FindAsync(id);
        }

        //Update car details
        public async Task UpdateCarDetails(Car car)
        {
            var existingCar = await context.cars.FindAsync(car.Id);
            if (existingCar != null)
            {
                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
                existingCar.PricePerDay = car.PricePerDay;
                existingCar.IsAvailable = car.IsAvailable;
                await context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Car with ID {car.Id} not found.");
            }
        }


        //delet car 
        public async Task DeleteCar(int id)
        {
            var car = await context.cars.FindAsync(id);
            if (car != null) 
            {
                context.cars.Remove(car); 
                await context.SaveChangesAsync(); 
            }
            else
            {
                throw new KeyNotFoundException($"Car with ID {id} not found.");
            }
        }



    }
}
