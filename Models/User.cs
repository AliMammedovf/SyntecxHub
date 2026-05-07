using System.ComponentModel.DataAnnotations;

namespace SyntecxhubUserApi.Models
{
    public class User:BaseEntity
    {
        


        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public List<Note> Notes { get; set; } = new();
    }
}
