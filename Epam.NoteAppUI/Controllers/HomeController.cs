namespace Epam.NoteAppUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotesBLL _notesBll;
        private readonly IAuthService _authService;

        public HomeController(ILogger<HomeController> logger, INotesBLL notesBLL, IAuthService authService)
        {
            _logger = logger;
            _notesBll = notesBLL;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var notes = _notesBll.GetNotes(true);

            return View(notes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string text, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                var username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                var user = _authService.GetUserByLogin(username);

                string defaultImagePath = "/images/Notes/default.png";
                string imagePath = defaultImagePath;

                if (image is not null)
                {
                    string filename = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/notes");
                    string filePath = Path.Combine(imagesFolderPath, filename);

                    using var fs = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(fs);

                    imagePath = "/images/Notes/" + filename;
                }

                _notesBll.AddNote(text, user, imagePath);
                
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        public IActionResult Edit(Guid id)
        {
            var note = _notesBll.GetNote(id);

            if (note is null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost]
        [Route("[controller]/editnote")]
        public IActionResult Edit(Guid id, string text)
        {
            if (ModelState.IsValid)
            {
                _notesBll.EditNote(id, text);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var note = _notesBll.GetNote(id);

            if (note is null)
            {
                return NotFound();
            }

            return View(id);
        }

        [HttpPost]
        [Route("[controller]/deletenote")]
        public IActionResult DeleteNote(Guid id)
        {
            var note = _notesBll.GetNote(id);

            if (note is null)
            {
                return NotFound();
            }

            var username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            var user = _authService.GetUserByLogin(username);
            _notesBll.RemoveNote(id, user);

            return RedirectToAction("Index");
        }
    }
}
