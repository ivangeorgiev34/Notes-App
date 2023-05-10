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
