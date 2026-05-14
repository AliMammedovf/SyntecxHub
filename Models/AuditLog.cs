namespace SyntecxhubUserApi.Models
{
    public class AuditLog: BaseEntity
    {

        public string Action { get; set; }

        public int PerformedByUserId { get; set; }

    }
}
