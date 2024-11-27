using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarRentalSystemAPI.Services;
using CarRentalSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarRentalSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        //GetUserbyId
        [HttpGet("userid/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user= await userService.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        //GetUserbyEmail
        [HttpGet("useremail/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }


        //Add user
        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }
            await userService.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), user.Id);
        }

        //delete a user
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await userService.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            await userService.DeleteUser(id);
            return Ok();
        } 



    }
}
