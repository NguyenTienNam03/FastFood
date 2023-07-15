using Microsoft.AspNetCore.Mvc;

namespace AppView.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
