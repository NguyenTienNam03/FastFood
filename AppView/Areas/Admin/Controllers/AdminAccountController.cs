using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AppView.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        private ICustomerService customerService;
        HttpClient client;
        public AdminAccountController()
        {
            customerService = new CustomerSevice();
            client = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Order()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            //if(claimsPrincipal.Identity.IsAuthenticated) 
            //{
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                var id = customerService.GetAllCus().FirstOrDefault(c => c.Email == email).IDCustomer;
                string url = $"https://localhost:7031/api/Cart/ShowCartDetail?id={id}";
                var repos = await client.GetAsync(url);
                var data = await repos.Content.ReadAsStringAsync();
                var orderbyid = JsonConvert.DeserializeObject<List<CartDetail>>(data);
              
            //}
           
            return View(orderbyid);
        }
    }
}
