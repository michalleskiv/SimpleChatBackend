using System;
using SimpleChat.Database.Models;

namespace SimpleChat.BusinessModel.Dto
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UserModel user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Token = token;
        }
    }
}