using Database;
using Microsoft.AspNetCore.Mvc;
using RecipeApplicationSQL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace RecipeApplicationSQL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Database.Database _database;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _database = new Database.Database("Data Source=localhost;Initial Catalog=RecipesDB;Integrated Security=True;Encrypt=False");
        }

        public IActionResult Index()
        {
            List<Recipe>recipe = _database.GetAllRecipes();
            return View(recipe);
        }

        public IActionResult Details(int id)
        {
            Recipe recipe = _database.GetRecipeById(id);
            return View(recipe);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
