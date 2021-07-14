using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.BusinessModel.Dto;
using SimpleChat.BusinessModel.Interfaces;
using SimpleChat.Database.Models;
using SimpleChat.Helpers;

namespace SimpleChat.Controllers
{
    [Route("/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<UserModel>> GetAllUsers() => await _userService.GetAllUsersAsync();

        [HttpGet]
        [Authorize]
        public UserModel GetUser()
        {
            return (UserModel) HttpContext.Items["User"];
        }

        [HttpPost]
        public async Task<UserModel> RegisterUser([FromBody] AuthenticateRequest authenticateRequest)
        {
            return await _userService.RegisterUserAsync(authenticateRequest);
        }
    }
}