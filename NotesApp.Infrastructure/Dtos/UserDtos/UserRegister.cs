using NotesApp.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Infrastructure.Dtos.UserDtos
{
    public class UserRegister
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(InfrastructureConstants.User.USER_USERNAME_MAX_LENGTH, MinimumLength = InfrastructureConstants.User.USER_USERNAME_MIN_LENGTH, ErrorMessage = "Username must be between 6 and 12 symbols!")]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(InfrastructureConstants.User.USER_PASSWORD_MAX_LENGTH, MinimumLength = InfrastructureConstants.User.USER_USERNAME_MIN_LENGTH, ErrorMessage = "Password must be between 6 and 12 symbols!")]
        public string Password { get; set; } = null!;

    }
}
