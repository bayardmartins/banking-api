using Banking.Core.Aggregate.Entities;

namespace Banking.Core.Services
{
    public class UserService
    {
        public User? ProcessUserLogin(User user, User? userFound)
        {
            bool userNotFound = userFound == null;
            bool wrongPassword = userNotFound ? true : user.Password != userFound.Password;
            if (userNotFound || wrongPassword) return null;
            return userFound;
        }

        public bool ValidateUserBeforeRegister(User user)
        {
            bool isValidLogin = !user.Login.Contains(' ') && user.Login.Length < 30;
            bool isValidPassword = !user.Password.Contains(' ') && user.Password.Length < 30;
            return isValidLogin && isValidPassword;
        }
    }
}