using Xunit;
using Banking.Core.Services;
using Banking.Core.Aggregate.Entities;

namespace Banking.Tests.UnitTests.Core
{
    public class UserService_Tests
    {
        [Fact]
        public void UserNotFound_ReturnNull()
        {
            UserService userService = new UserService();
            User user1 = new User();
            user1.Login = "test";
            user1.Password = "pass";
            User? user2 = null;
            var result = userService.ProcessUserLogin(user1,user2);

            Assert.Equal(null, result);
        }

        [Fact]
        public void UserFoundWrongPassword_ReturnNull()
        {
            UserService userService = new UserService();
            User user1 = new User();
            user1.Login = "test";
            user1.Password = "pass";
            User user2  = new User();
            user1.Login = "test";
            user2.Password = "password";
            var result = userService.ProcessUserLogin(user1,user2);

            Assert.Equal(null, result);
        }

        [Fact]
        public void UserFound_ReturnUser()
        {
            UserService userService = new UserService();
            User user1 = new User();
            user1.Login = "test";
            user1.Password = "pass";
            User user2  = new User();
            user1.Login = "test";
            user2.Password = "pass";
            var result = userService.ProcessUserLogin(user1,user2);

            Assert.Equal(user2, result);
        }

        //ValidateUserBeforeRegister
    }
}