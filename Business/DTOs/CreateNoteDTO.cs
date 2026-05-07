using System.ComponentModel.DataAnnotations;

namespace SyntecxhubUserApi.Business.DTOs
{
    public class CreateNoteDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
