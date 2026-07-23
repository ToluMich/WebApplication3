namespace WebApplication3.Models
{
    public class FetchUsersModel
    {

        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;

        public string? Gender { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
