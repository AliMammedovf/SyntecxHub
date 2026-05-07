namespace SyntecxhubUserApi.Models
{
    public class Note:BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public bool IsDeleted { get; set; } = false;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
