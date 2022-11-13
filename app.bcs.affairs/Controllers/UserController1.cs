using Microsoft.AspNetCore.Mvc;

namespace app.bcs.affairs.Controllers
{
    public class UserController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
