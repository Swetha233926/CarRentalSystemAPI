using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarRentalSystemAPI.Models;
using CarRentalSystemAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace CarRentalSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : ControllerBase
    {
        private readonly ICarRentalService carRentalService;

        public CarRentalController(ICarRentalService carRentalService)
        {
            this.carRentalService = carRentalService;
        }

        // Get all cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
        {
            var cars = await carRentalService.GetAllCars();
            if (cars == null || !cars.Any())
            {
                return NotFound("No cars found.");
            }
            return Ok(cars);
        }

        // Add Car
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<Car>> AddCar(Car car)
        {
            if (car == null)
            {
                return BadRequest("Car details cannot be null.");
            }

            await carRentalService.AddCar(car);
            return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
        }

        // Get Car By ID
        [HttpGet("Getcarbyid/{id}")]
        public async Task<ActionResult<Car>> GetCarById(int id)
        {
            var car = await carRentalService.GetCarById(id);
            if (car == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }

            return Ok(car);
        }

        // Check car availability based on the ID
        [HttpGet("availability/{id}")]
        public async Task<IActionResult> CheckCarAvailability(int id)
        {
            var isAvailable = await carRentalService.CheckCarAvailability(id);
            if (!isAvailable)
            {
                return NotFound($"Car with ID {id} is unavailable or does not exist.");
            }

            return Ok($"Car with ID {id} is available for rent.");
        }

        // Update Car details
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCarDetails(int id, [FromBody] Car car)
        {
            if (car == null)
            {
                return BadRequest("Car details cannot be null.");
            }

            if (id != car.Id)
            {
                return BadRequest("Car ID in the route does not match the ID in the body.");
            }

            var existingCar = await carRentalService.GetCarById(id);
            if (existingCar == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }

            await carRentalService.UpdateCarDetails(car);
            return Ok($"Car with ID {id} updated successfully.");
        }

        // Delete car
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await carRentalService.GetCarById(id);
            if (car == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }

            await carRentalService.DeleteCar(id);
            return Ok($"Car with ID {id} deleted successfully.");
        }

        // Rent car
        [HttpPost("rent")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RentCar([FromQuery] int carId, [FromQuery] int userId, [FromQuery] int rentalDays)
        {
            if (rentalDays <= 0)
            {
                return BadRequest("Rental days must be greater than 0.");
            }

            var result = await carRentalService.RentCar(carId, userId, rentalDays);

            // Check the 'Value' property of the result tuple to see if the rental was successful
            if (!result.Value)
            {
                if (result.PricePerDay == 0)  // This means the car doesn't exist
                {
                    return BadRequest("Car with the provided ID does not exist.");
                }
                else if (result.PricePerDay == -1) // You could return -1 for unavailable cars
                {
                    return BadRequest("Car is unavailable at the moment.");
                }
            }

            return Ok($"Car rented successfully for {rentalDays} days, and rental Price {rentalDays * result.PricePerDay}");
        }

    }
}
