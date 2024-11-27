using CarRentalSystemAPI.Data;
using CarRentalSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystemAPI.Repositories
{
    public interface IUserRepository
    {
        public Task AddUser(User user);
        public Task<User> GetUserById(int id);
        public Task<User> GetUserByEmail(string email);
        public Task DeleteUser(int id);
    }
    public class UserRepository:IUserRepository
    {
        private readonly CarDbContext context;
        public UserRepository(CarDbContext context)
        {
            this.context = context;
        }

        //Adding the user
        public async Task AddUser(User user)
        {
            await context.users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        //get user by id
        public async Task<User> GetUserById(int id)
        {
            return await context.users.FindAsync(id);
           
        }

        //get user by email
        public async Task<User> GetUserByEmail(string email)
        {
            return await context.users.FirstOrDefaultAsync(u => u.User_email == email);
        }


        //delete user by Id
        public async Task DeleteUser(int id)
        {
            var user = await context.users.FindAsync(id);
            if (user != null)
            {
                context.users.Remove(user);
                context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
        }
    }
}
