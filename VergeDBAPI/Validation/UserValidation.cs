using Microsoft.EntityFrameworkCore;
using VergeDBAPI.Models;

namespace VergeDBAPI.Validation
{
    public static class UserValidation
    {

        public static async Task<string> Validate(VergeDBAPIContext context, UserData userData)
        {
            try
            {
                await context.Users.Where(n => n.Username.Equals(userData.UserName)).FirstAsync();
            }
            catch (InvalidOperationException e)
            {
                return "Users not registered";
            }

            try
            {
                if (userData.UserRole == OrgRole.Banned)
                {
                    return "Users is banned";
                }
            }
            catch (ArgumentNullException e)
            {
                return "No user specified";
            }

            return null;
        }
    }
}
