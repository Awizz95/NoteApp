using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Epam.NoteAppUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public LoginController(IMapper mapper, IAuthService authService)
        {
            _mapper = mapper;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string defaultImagePath = "/images/Profiles/default.png";
            string imagePath = defaultImagePath;

            if (model.ProfilePhoto is not null)
            {
                string filename = Guid.NewGuid().ToString() + "_" + model.ProfilePhoto.FileName;
                string imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profiles");
                string filePath = Path.Combine(imagesFolderPath, filename);

                using var fs = new FileStream(filePath, FileMode.Create);
                await model.ProfilePhoto.CopyToAsync(fs);

                imagePath = "/images/Profiles/" + filename;
            }

            var user = _mapper.Map<User>(model);
            user.ProfilePhoto = imagePath;

            var result = _authService.RegisterUser(user);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(model);
            var person = _authService.Login(user);

            if (person is null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, person.Login),
                new(ClaimTypes.Role, person.Role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
