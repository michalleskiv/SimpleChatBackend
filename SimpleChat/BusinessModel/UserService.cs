using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleChat.BusinessModel.Dto;
using SimpleChat.BusinessModel.Interfaces;
using SimpleChat.Database;
using SimpleChat.Database.Models;
using SimpleChat.Helpers;

namespace SimpleChat.BusinessModel
{
    public class UserService : IUserService
    {
        private readonly ChatContext _chatContext;
        private readonly AppSettings _appSettings;

        public UserService(ChatContext chatContext, IOptions<AppSettings> appSettings)
        {
            _chatContext = chatContext;
            _appSettings = appSettings.Value;
        }
        
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = _chatContext.Users.SingleOrDefault(x => 
                x.Username == model.Username && x.PasswordHash == HashPassword(model.Password));

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);
            user.Token = token;
            await _chatContext.SaveChangesAsync();

            return new AuthenticateResponse(user, token);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync() => await _chatContext.Users.ToListAsync();
        
        public async Task<IEnumerable<AuthenticateRequest>> GetAllUsersAsDtoAsync() =>
            await _chatContext.Users.Select(u => new AuthenticateRequest
            {
                Username = u.Username,
                Password = u.PasswordHash
            }).ToListAsync();

        public async Task<UserModel> GetUserAsync(AuthenticateRequest authenticateRequest)
        {
            var userModel = await _chatContext.Users.SingleOrDefaultAsync(u =>
                u.Username == authenticateRequest.Username && u.PasswordHash == HashPassword(authenticateRequest.Password));
            
            return userModel;
        }

        public async Task<UserModel> RegisterUserAsync(AuthenticateRequest authenticateRequest)
        {
            if (await GetUserAsync(authenticateRequest) != null)
                return null;

            var userModel = new UserModel
            {
                Username = authenticateRequest.Username,
                PasswordHash = HashPassword(authenticateRequest.Password)
            };

            var res = _chatContext.Users.Add(userModel);
            await _chatContext.SaveChangesAsync();

            return res.Entity;
        }

        public async Task<UserModel> GetById(Guid id) => 
            await _chatContext.Users.SingleOrDefaultAsync(u => u.Id == id);

        private string HashPassword(string password)
        {
            try
            {
                using var sha = SHA256.Create();
                var passwordInBytes = Encoding.UTF8.GetBytes(password);
                var passwordHashInBytes = sha.ComputeHash(passwordInBytes);
                return Encoding.UTF8.GetString(passwordHashInBytes);
            }
            catch (Exception)
            {
                return password;
            }
        }

        private string GenerateJwtToken(UserModel user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}