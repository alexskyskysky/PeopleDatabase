using Microsoft.AspNetCore.Mvc;

namespace PeopleDatabase.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Redirect("/index.html");
        }
    }
}
