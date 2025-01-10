using MealOrderingApp.Server.Services.Infrastruce;
using MealOrderingApp.Server.Services.Services;
using MealOrderingApp.Shared.DTOs;
using MealOrderingApp.Shared.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MealOrderingApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Login")]
        public async Task<UserLoginResponseDTO> Login(UserLoginRequestDTO userLoginRequestDTO)
        {

            UserLoginResponseDTO response = await _userService.Login(userLoginRequestDTO);
            return response;
        }

        [HttpGet("Users")]
        public async Task<ServiceResponse<List<UserDTO>>> GetUsers()
        {
            return new ServiceResponse<List<UserDTO>>()
            {
                Value = await _userService.GetUsers()
            };
        }
        [HttpPost("CreateUser")]
        public async Task<ServiceResponse<bool>> CreateUser(UserDTO user)
        {
            return new ServiceResponse<bool>()
            {
                Value = await _userService.CreateUser(user)
            };
        }

    }
}
