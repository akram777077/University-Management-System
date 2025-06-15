using Domain.Entities;

namespace Applications.Helpers
{
    public static class PasswordGenerator
    {
        public static string SetPassword(this User user, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(this User user, string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify (password, hashedPassword);
        }
    }
}
