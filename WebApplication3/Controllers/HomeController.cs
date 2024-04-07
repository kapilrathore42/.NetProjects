using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using WebApplication3.DB;
using WebApplication3.Models;
using static Azure.Core.HttpHeader;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Welcome to my website!";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    ViewBag.LoginSuccess = true;
                    // Successful login, redirect to dashboard or home page
                    return RedirectToAction("Index", "Home"); // Assuming there's a Dashboard controller
                }
                else
                {
                   ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the username is already taken
                if (_context.Users.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError(string.Empty, "Username is already taken.");
                    return View(model);
                }

                // Create new user in the database
                var newUser = new User
                {
                    Username = model.Username,
                    Password = model.Password // Note: In production, hash and salt the password
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // Redirect to login or dashboard
                return RedirectToAction("Login", "Home");
            }

            // If ModelState is invalid, return the signup view with errors
            return View(model);
        }

        [HttpGet]
        public IActionResult AddNotes()
        {
            return View("Note");
        }
        [HttpPost]

        [HttpPost]
        public IActionResult AddNotes(NoteModel ob)
        {
            if (ModelState.IsValid) // Check if model state is valid
            {
                _context.Notes.Add(ob);
                _context.SaveChanges();

              //  return RedirectToAction("Index"); // Redirect to another action (e.g., Index)
            }

            // If model state is not valid, return the same view with validation errors
            return View("Note");
        }

        [HttpGet]
        public IActionResult ViewNotes()
        {
            var notes = _context.Notes.ToList(); // Retrieve all notes from the database
            return View(notes);
        }
        public IActionResult SearchNotes(string searchTerm)
        {
            var matchedNotes = _context.Notes
                                     .Where(n => n.Note.Contains(searchTerm))
                                     .ToList();

            return View("ViewNotes", matchedNotes); // Display the matched notes in the ViewNotes view
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
