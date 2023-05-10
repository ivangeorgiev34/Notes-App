using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Infrastructure.Constants
{
    public static class InfrastructureConstants
    {
        public static class User
        {
            public const int USER_USERNAME_MIN_LENGTH = 6;
            public const int USER_USERNAME_MAX_LENGTH = 12;

            public const int USER_PASSWORD_MIN_LENGTH = 6;
            public const int USER_PASSWORD_MAX_LENGTH = 12;
        }
    }
}
