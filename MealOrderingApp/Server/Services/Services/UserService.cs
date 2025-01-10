using AutoMapper;
//using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;

using AutoMapper.QueryableExtensions;
using MealOrderingApp.Server.Data.Context;
using MealOrderingApp.Server.Data.Models;
using MealOrderingApp.Server.Services.Infrastruce;
using MealOrderingApp.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MealOrderingApp.Server.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public UserService(IMapper Mapper, DataContext Context, IConfiguration Configuration, UserManager<User> userManager, ITokenHandler tokenHandler)
        {
            _mapper = Mapper;
            _context = Context;
            _configuration = Configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }


        public async Task<bool> CreateUser(UserDTO user)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = user.FirstName,
                UserName = user.Username,
                Email = user.Email,
                LastName = user.LastName
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
                return true;

            // Xətaları yığırıq və istifadəçiyə dəqiq məlumat veririk
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"User creation failed: {errors}");
        }

        public async Task<bool> DeleteUserById(string id)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);

            if (dbUser == null)
                throw new Exception("User not found");

            _context.Users.Remove(dbUser);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<UserDTO> GetUserById(string id)
        {
            return await _context.Users
                       .Where(i => i.Id == id)
                       .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                       .FirstOrDefaultAsync();
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            return await _context.Users
                          .Where(i => !i.IsActive)
                          .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                          .ToListAsync();
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginRequestDTO userLoginRequestDTO)
        {
            // 1. İstifadəçi adı ilə tapmağa cəhd edin
            var user = await _userManager.FindByNameAsync(userLoginRequestDTO.UsernameOrEmail);

            // 2. Əgər istifadəçi adı ilə tapılmadısa, email ilə tapmağa cəhd edin
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userLoginRequestDTO.UsernameOrEmail);
            }

            // 3. İstifadəçi tapılmadıqda, istisna atılır
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or email");
            }

            // 4. Şifrəni doğrulayın
            var result = await _userManager.CheckPasswordAsync(user, userLoginRequestDTO.Password);
            if (!result)
            {
                throw new UnauthorizedAccessException("Invalid username/email or password");
            }

            //// 5. JWT Token yaradın
            //var token = GenerateJwtToken(user);

            // 6. Geri qaytarılacaq məlumatları hazırlamaq
            var response = new UserLoginResponseDTO
            {
                Token = _tokenHandler.GenerateJwtToken(user),
                User = _mapper.Map<UserDTO>(user),
            };

            return response;
        }

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            var dbUser = await _context.Users.Where(i => i.Id == user.Id).FirstOrDefaultAsync();

            if (dbUser == null)
                throw new Exception("User not found");


            _mapper.Map(user, dbUser);

            int result = await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(dbUser);

        }


    }
}
