namespace Epam.NoteAppUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotesBLL _notesBll;
        private readonly IAuthService _authService;

        public AccountController(ILogger<HomeController> logger, INotesBLL notesBLL, IAuthService authService)
        {
            _logger = logger;
            _notesBll = notesBLL;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Users()
        {
            var users = _authService.GetAllUsers();
            ViewBag.Role = HttpContext.User.FindFirst(ClaimTypes.Role).Value;

            return View(users);
        }

        [HttpPost]
        public IActionResult UserNotes(Guid id)
        {
            var user = _authService.GetUserById(id);
            ViewBag.User = user;

            var guids = user.Notes;
            var notes = _notesBll.GetNotes(true).Where(x => guids.Contains(x.ID)).ToList();

            return View(notes);
        }

        [HttpPost]
        public IActionResult DeleteUser(Guid id)
        {
            var result = _authService.DeleteUser(id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Users");
        }
    }
}
