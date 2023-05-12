using NotesApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Core.Contracts
{
    public interface IUserService
    {
        Task<User?> UserMatchesCredentialsAsync(string email, string password);

        Task RegisterUserAsync(string email, string username, string password);

        Task<bool> UserExistsByEmailAsync(string email );

        Task<User?> GetUserByIdAsync(Guid id);
    }
}
