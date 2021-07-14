using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleChat.BusinessModel.Dto;
using SimpleChat.Database.Models;

namespace SimpleChat.BusinessModel.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<IEnumerable<AuthenticateRequest>> GetAllUsersAsDtoAsync();
        Task<UserModel> GetUserAsync(AuthenticateRequest authenticateRequest);
        Task<UserModel> RegisterUserAsync(AuthenticateRequest authenticateRequest);
        Task<UserModel> GetById(Guid id);
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    }
}