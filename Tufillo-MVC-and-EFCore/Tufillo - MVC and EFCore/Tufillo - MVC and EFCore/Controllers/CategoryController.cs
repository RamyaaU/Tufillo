using Microsoft.AspNetCore.Mvc;

namespace Tufillo___MVC_and_EFCore.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
