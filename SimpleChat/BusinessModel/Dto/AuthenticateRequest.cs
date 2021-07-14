using System.ComponentModel.DataAnnotations;

namespace SimpleChat.BusinessModel.Dto
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}