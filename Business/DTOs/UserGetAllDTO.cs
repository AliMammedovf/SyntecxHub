using System.ComponentModel.DataAnnotations;

namespace SyntecxhubUserApi.Business.DTOs
{
    public class UserGetAllDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<UserGetOneDTO> UserList { get; set; }


        public UserGetAllDTO()
        {
            UserList = new List<UserGetOneDTO>();
        }
    }
}
