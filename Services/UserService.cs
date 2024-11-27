using CarRentalSystemAPI.Models;
using CarRentalSystemAPI.Repositories;
namespace CarRentalSystemAPI.Services
{
    public interface IUserService
    {
        public Task AddUser(User user);
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserById(int id);
        public Task DeleteUser(int id);

    }
    public class UserService:IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddUser(User user)
        {
           await userRepository.AddUser(user);
        }

        public async Task<User> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await userRepository.GetUserByEmail(email);
        }


        public async Task DeleteUser(int id)
        {
            await userRepository.DeleteUser(id);
        }
    }
}
