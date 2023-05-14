using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Core.Contracts;
using NotesApp.Infrastructure.Common;
using NotesApp.Infrastructure.Dtos.UserDtos;
using NotesApp.Infrastructure.Models;
using System.Security.Cryptography;
using System.Text;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            this.userService = _userService;
        }

        /// <summary>
        /// logs in a existing user
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var providedPassword = HashPassword(userLogin.Password);

            var loggedInUser = await userService.UserMatchesCredentialsAsync(userLogin.Email, providedPassword);

            if (loggedInUser == null)
            {
                return Unauthorized(new {Message = "Wrong email or password" });
            }

            return Ok(loggedInUser);

        }

        /// <summary>
        /// registers a user if he doesn't exist
        /// </summary>
        /// <param name="userRegister"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            if (await userService.UserExistsByEmailAsync(userRegister.Email))
            {
                return BadRequest(new {Message= "User with this email already exists" });
            }

            if (await userService.UserExistsByUsernameAsync(userRegister.Username))
            {
                return BadRequest(new { Message = "User with this username already exists" });
            }

            var hashedPassword = HashPassword(userRegister.Password);

            await userService.RegisterUserAsync(userRegister.Email, userRegister.Username, hashedPassword);

            return Ok(new {Username = userRegister.Username});
        }

        /// <summary>
        /// method that hashes passwords
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the password string to bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash value of the password bytes
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash bytes to a hexadecimal string
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return hashedPassword;
            }
        }

    }
}
