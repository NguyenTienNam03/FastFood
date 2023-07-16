using AppData.IService;
using AppData.Models;
using AppData.Service;
using AppData.ViewModels;
using AppView.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Newtonsoft.Json;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.Security.Claims;
using System.Text;
using ZXing.QrCode.Internal;

namespace AppView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClient client = new HttpClient();
        private ICustomerService customerService;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        [HttpGet]
        public async Task<IActionResult> DetailCombo(Guid id)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/ShowComboFFByID?id={id}";
            var respon = await client.GetAsync(url);
            var data = await respon.Content.ReadAsStringAsync();
            ComboFastFoodViewModel Combo = JsonConvert.DeserializeObject<ComboFastFoodViewModel>(data);
            return View(Combo);
        }
        [HttpGet]
        public async Task<IActionResult> DetailDrink(Guid id)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/GetByID?id={id}";
            var respon = await client.GetAsync(url);
            var data = await respon.Content.ReadAsStringAsync();
            Drinks drinks = JsonConvert.DeserializeObject<Drinks>(data);

            return View(drinks);
        }
        [HttpGet]
        public async Task<IActionResult> DetailMain(Guid id)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/GetMainDishesById?id={id}";
            var respon = await client.GetAsync(url);
            var data = await respon.Content.ReadAsStringAsync();
            MainDishes main = JsonConvert.DeserializeObject<MainDishes>(data);
            return View(main);
        }
        [HttpGet]
        public async Task<IActionResult> GetSideDetail(Guid id)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/GetSideDishesByid?id={id}";
            var respon = await client.GetAsync(url);
            var data = await respon.Content.ReadAsStringAsync();
            var sides = JsonConvert.DeserializeObject<SideDishes>(data);
            return View(sides);
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> AddToCart(CartDetail cartDetail)
        {

            ClaimsPrincipal claimsPrincipal = HttpContext.User;


            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}