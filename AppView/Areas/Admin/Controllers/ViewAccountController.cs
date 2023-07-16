using AppData.IService;
using AppData.Models;
using AppData.Service;
using AppData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class ViewAccountController : Controller
    {

        HttpClient client = new HttpClient();
        private ICustomerService customerService; 
        public ViewAccountController()
        {
            customerService = new CustomerSevice();
        }
        public async Task<IActionResult> Index()
        {
            string UrlCombo = "https://localhost:7031/api/ComboFastFood/ShowComboFF";
            string UrlDrink = "https://localhost:7031/api/ComboFastFood/GetAllDrink";
            string UrlSide = "https://localhost:7031/api/ComboFastFood/GetAllSidedishes";
            string UrlMain = "https://localhost:7031/api/ComboFastFood/GetAllMainDihes";



            Task<HttpResponseMessage> reposcombo = client.GetAsync(UrlCombo);
            Task<HttpResponseMessage> reposdrink = client.GetAsync(UrlDrink);
            Task<HttpResponseMessage> reposside = client.GetAsync(UrlSide);
            Task<HttpResponseMessage> reposmain = client.GetAsync(UrlMain);

            Task.WhenAll(reposcombo, reposdrink, reposside, reposmain);

            if (reposcombo.Result.IsSuccessStatusCode)
            {
                var result1 = reposcombo.Result.Content.ReadAsStringAsync();
                var datacombo = JsonConvert.DeserializeObject<List<ComboFastFoodViewModel>>(await result1);
                ViewBag.Combo = datacombo;
            }
            if (reposdrink.Result.IsSuccessStatusCode)
            {
                var result2 = reposdrink.Result.Content.ReadAsStringAsync();
                var datadrink = JsonConvert.DeserializeObject<List<Drinks>>(await result2);
                ViewBag.Dink = datadrink;
            }
            if (reposside.Result.IsSuccessStatusCode)
            {
                var result3 = reposside.Result.Content.ReadAsStringAsync();
                var dataSide = JsonConvert.DeserializeObject<List<SideDishes>>(await result3);
                ViewBag.Side = dataSide;
            }
            if (reposmain.Result.IsSuccessStatusCode)
            {
                var result4 = reposmain.Result.Content.ReadAsStringAsync();
                var dataMain = JsonConvert.DeserializeObject<List<MainDishes>>(await result4);
                ViewBag.Main = dataMain;
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(CartDetail cartDetail)
        {

            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            //if (claimsPrincipal.Identity.IsAuthenticated)
            //{
                var user = HttpContext.User;
                var email = user.FindFirstValue(ClaimTypes.Email);
                //if (customerService.GetAllCus().Any(c => c.Email == email))
                //{
                    var idcustomer = customerService.GetAllCus().FirstOrDefault(c => c.Email == email).IDCustomer;
                    string url = $"https://localhost:7031/api/Cart/AddToCart?idfood={cartDetail.IDFood}&idcus={idcustomer}";
                    var obj = JsonConvert.SerializeObject(cartDetail);
                    StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
                    HttpResponseMessage message = await client.PostAsync(url, content);
                    //if (message.IsSuccessStatusCode)
                    //{
                        return RedirectToAction("Order" , "AdminAccountController");
                    //} else
                    //{
                    //    return RedirectToAction("Order", "AdminAccountController");
                    //}
                //}
                //return RedirectToAction("Order", "AdminAccountController");
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Login");


            //}
        }

        public async Task<IActionResult> AddToCart(Guid idfood , Guid idcus)
        {
            string url = $"https://localhost:7031/api/Cart/AddToCart?idfood=2d608d0f-134c-4490-9203-abb7d6323493&idcus=7c0c20d5-9d83-47b7-b8da-da200ad01250";
            return View();
        }

    }
}
