using Microsoft.AspNetCore.Mvc;

namespace AppView.Areas.Customer.Controllers
{
    public class CustomerAccountController : Controller
    {
        public async Task<IActionResult> Cart()
        {
            return View();
        }
    }
}
