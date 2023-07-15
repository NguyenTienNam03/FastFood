using AppData.Models;
using AppData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppView.Areas.Admin.Controllers
{
    public class ViewAccountController : Controller
    {

        HttpClient client = new HttpClient();
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

    }
}
