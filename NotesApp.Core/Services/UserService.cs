using Microsoft.EntityFrameworkCore;
using NotesApp.Core.Contracts;
using NotesApp.Infrastructure.Common;
using NotesApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository _repository)
        {
            this.repository = _repository;
        }

        /// <summary>
        /// gets user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await repository.All<User>()
                .Include(u=>u.Notes)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// registers a non-existing user in the database
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public async Task RegisterUserAsync(string email, string username, string password)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Username = username,
                Password = password
            };

           await  repository.AddAsync<User>(user);

           await repository.SaveChangesAsync();
        }

        /// <summary>
        /// checks if there is a user in the database with the given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            if(await repository.All<User>().AnyAsync(u=>u.Email == email))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// checks if user exists by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> UserExistsByUsernameAsync(string username)
        {
            if (await repository.All<User>().AnyAsync(u => u.Username == username))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// checks if a given user matches the given credentials
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public async Task<User?> UserMatchesCredentialsAsync(string email, string password)
        {
            var user = await repository.All<User>()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.Password == password);

            if (user != null)
            {
                return user;
            }

            return null!;
        }
    }
}
