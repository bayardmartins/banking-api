using Microsoft.AspNetCore.Mvc;
using Banking.Core.Interfaces.Repositories;
using Banking.Core.Services;
using Banking.Core.Aggregate.Entities;
using Banking.Web.Services;
using Banking.Core.Utils;

namespace Banking.Web.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly UserService userService;

        public LoginController(IUserRepository repository)
        {
            _repository = repository;
            userService = new UserService();
        }

        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult<dynamic>> Authenticate(User user)
        {
            User? userFound = _repository.GetUserByLogin(user.Login);
            var processedUser = userService.ProcessUserLogin(user,userFound);
            
            if (processedUser == null) return BadRequest(LoginMessageConstant.LoginError);

            var token = TokenService.GenerateToken(processedUser);

            return new 
            {
                user = processedUser.Login,
                token = token
            };
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<dynamic>> Registrate(User user)
        {
            try
            {
                User? userFound = _repository.GetUserByLogin(user.Login);
                if (userFound != null) { return BadRequest(LoginMessageConstant.UserAlreadyExists); }
                if (!userService.ValidateUserBeforeRegister(user)) { return UnprocessableEntity(LoginMessageConstant.InvalidUserData); }
                int id = await _repository.Register(user);
                return Ok(new { message = MessageConstant.ItemCreated(typeof(User),id),
                                id = id});
            }
            catch
            {
                return BadRequest(MessageConstant.ItemNotCreated(typeof(User)));
            }
        }
    }
}