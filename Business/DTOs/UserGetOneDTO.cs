using System.ComponentModel.DataAnnotations;

namespace SyntecxhubUserApi.Business.DTOs
{
    public class UserGetOneDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Role { get; set; }
    }
}
