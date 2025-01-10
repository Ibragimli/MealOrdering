using MealOrderingApp.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MealOrderingApp.Server.Services.Infrastruce
{
    public interface IUserService
    {
        public Task<UserDTO> GetUserById(string id);

        public Task<List<UserDTO>> GetUsers();

        public Task<bool> CreateUser(UserDTO user);

        public Task<UserDTO> UpdateUser(UserDTO user);

        public Task<bool> DeleteUserById(string id);

        public Task<UserLoginResponseDTO> Login(UserLoginRequestDTO userLoginRequestDTO);
    }

}
