namespace Epam.NoteAppUI.Models
{
    public class UserViewModel
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
    }
}
