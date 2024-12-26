namespace Epam.Auth.Models
{
    public class User
    {
        public Guid Id { get; init; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public RoleEnum Role { get; set; }
        public string? ProfilePhoto { get; set; }
        public List<Guid> Notes { get; set; } = new();
    }
}
