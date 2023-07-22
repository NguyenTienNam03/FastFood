using AppData.IService;
using AppData.Models;
using AppData.Service;
using AppData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Security.Policy;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        private DB_Context _context;
        private IDrinkService _drinkservice;
        private IMainDishesService _maindishesservice;
        private ISideDishesService _sidedishesservice;
        HttpClient client = new HttpClient();
        public ManageController()
        {
            _context = new DB_Context();
            _drinkservice = new DrinkService();
            _maindishesservice = new MainDishesService();
            _sidedishesservice = new SideDishesService();
        }
        [HttpGet]
        public IActionResult ManageCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ManageCustomer(AppData.Models.Customer customer)
        {
            return View();
        }
        // Manage ComboFastFood
        [HttpGet]
        public async Task<IActionResult> ManageCombo()
        {
            string url = "https://localhost:7031/api/ComboFastFood/ShowComboFF";
            var respon = await client.GetAsync(url);
            var data = await respon.Content.ReadAsStringAsync();
            var combo = JsonConvert.DeserializeObject<List<ComboFastFoodViewModel>>(data);
            return View(combo);
        }
        [HttpGet]
        public async Task<IActionResult> GetComboByID(Guid id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateCombo()
        {
            ViewBag.dirnk = new SelectList(_context.drinks.ToList().Where(c => c.Status == 1).OrderBy(c => c.NameDrink), "IDDrink", "NameDrink");
            ViewBag.main = new SelectList(_context.mainDishes.ToList().Where(c => c.Status == 1).OrderBy(c => c.NameMainDishes), "IDMainDishes", "NameMainDishes");
            ViewBag.side = new SelectList(_context.sideDishes.ToList().Where(c => c.Status == 1).OrderBy(c => c.NameSideDishes), "IDSideDishes", "NameSideDishes");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCombo(ComboFastFood comboFastFood)
        {
            //ViewBag.dirnk = new SelectList(_context.drinks.ToList().Where(c => c.Status == 1).OrderBy(c => c.NameDrink), "IDDrink", "NameDrink");
            //ViewBag.main = new SelectList(_context.mainDishes.ToList().Where(c => c.Status == 1).OrderBy(c => c.NameMainDishes), "IDMainDishes", "NameMainDishes");
            //ViewBag.side = new SelectList(_context.sideDishes.ToList().Where(c => c.Status == 1).OrderBy(c => c.NameSideDishes), "IDSideDishes", "NameSideDishes");
            if (comboFastFood.IDDrink == null)
            {
                AddNoDrink(comboFastFood);
                return RedirectToAction("ManageCombo", "Manage");
            }
            else if (comboFastFood.IDMainDishes == null)
            {
                AddNoMain(comboFastFood);
                return RedirectToAction("ManageCombo", "Manage");
            }
            else if (comboFastFood.IDSideDishes == null)
            {
                AddNoSide(comboFastFood);
                return RedirectToAction("ManageCombo", "Manage");

            }
            else if (comboFastFood.IDDrink != null && comboFastFood.IDMainDishes != null && comboFastFood.IDSideDishes != null)
            {
                AddFullComBo(comboFastFood);
                return RedirectToAction("ManageCombo", "Manage");
            }
            else
            {
                return RedirectToAction("CreateCombo", "Manage");
            }
        }

        private bool AddFullComBo(ComboFastFood comboFastFood)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/AddCombo?idside={comboFastFood.IDSideDishes}&idmain={comboFastFood.IDMainDishes}&iddrink={comboFastFood.IDDrink}&name={comboFastFood.NameCombo}&anh={comboFastFood.Image}&mota={comboFastFood.DescriptionCombo}&trangthai=1";
            var obj = JsonConvert.SerializeObject(comboFastFood);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = client.PostAsync(url, content).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {

                return true;

            }
            else
            {
                return false;
            }
        }
        private async Task<bool> AddNoDrink(ComboFastFood comboFastFood)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/AddCombo?idside={comboFastFood.IDSideDishes}&idmain={comboFastFood.IDMainDishes}&name={comboFastFood.NameCombo}&anh={comboFastFood.Image}&mota={comboFastFood.DescriptionCombo}&trangthai=1";
            var obj = JsonConvert.SerializeObject(comboFastFood);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync(url, content);


                return true;

        }
        private bool AddNoMain(ComboFastFood comboFastFood)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/AddCombo?idside={comboFastFood.IDSideDishes}&iddrink={comboFastFood.IDDrink}&name={comboFastFood.NameCombo}&anh={comboFastFood.Image}&mota={comboFastFood.DescriptionCombo}&trangthai=1";
            var obj = JsonConvert.SerializeObject(comboFastFood);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = client.PostAsync(url, content).Result;

                return true;

          
        }
        private bool AddNoSide(ComboFastFood comboFastFood)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/AddCombo?idmain={comboFastFood.IDMainDishes}&iddrink={comboFastFood.IDDrink}&name={comboFastFood.NameCombo}&anh={comboFastFood.Image}&mota={comboFastFood.DescriptionCombo}&trangthai=1";
            var obj = JsonConvert.SerializeObject(comboFastFood);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = client.PostAsync(url, content).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {

                return true;

            }
            else
            {
                return false;
            }
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> UpdateCombo(ComboFastFood comboFastFood)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ComboFastFood comboFastFood)
        {
            return View();
        }
        // Manage Drink

        //
        [HttpGet]
        public async Task<IActionResult> ShowAllDrink()
        {
            string url = $"https://localhost:7031/api/ComboFastFood/GetAllDrink";
            var respon = await client.GetAsync(url);
            var data = await respon.Content.ReadAsStringAsync();
            var lstDrink = JsonConvert.DeserializeObject<List<Drinks>>(data);
            return View(lstDrink);
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> AddDrink(Drinks drinks)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/CreateDrink?name={drinks.NameDrink}&image={drinks.Image}&gia={drinks.Price}&khoiluong={drinks.Mass}&trangthai=1";
            var obj = JsonConvert.SerializeObject(drinks);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowAllDrink", "Manage");
            }
            else
            {
                return View();
            }
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
        public async Task<IActionResult> UpdateDrink(Guid id)
        {

            string url = $"https://localhost:7031/api/ComboFastFood/GetByID?id={id}";
            var respon = await client.GetAsync(url);
            string data = await respon.Content.ReadAsStringAsync();
            Drinks drinks = JsonConvert.DeserializeObject<Drinks>(data);
            return View(drinks);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateDrink(Drinks drinks)
        {
            string urlupdate = $"https://localhost:7031/api/ComboFastFood/UpdateDrink?id={drinks.IDDrink}&name={drinks.NameDrink}&image={drinks.Image}&gia={drinks.Price}&khoiluong={drinks.Mass}&trangthai={drinks.Status}";

            var obj = JsonConvert.SerializeObject(drinks);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage updateobj = await client.PutAsync(urlupdate, content);
            if (updateobj.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowAllDrink", "Manage");
            }
            return RedirectToAction("UpdateDrink", "Manage");
        }

        public async Task<IActionResult> DeleteDrink(Drinks drinks)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/DeleteDrink?id={drinks.IDDrink}";
            var obj = JsonConvert.SerializeObject(drinks);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage Delete = await client.PutAsync(url, content);

            return RedirectToAction("ShowAllDrink", "Manage");
        }

        // Manage MainDishes

        [HttpGet]
        public async Task<IActionResult> GetAllMainDishes()
        {
            string url = $"https://localhost:7031/api/ComboFastFood/GetAllMainDihes";
            var respon = await client.GetAsync(url);
            var data = await respon.Content.ReadAsStringAsync();
            var main = JsonConvert.DeserializeObject<List<MainDishes>>(data);
            return View(main);
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
        public async Task<IActionResult> AddMain()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMain(MainDishes mainDishes)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/CreateMainDishes?name={mainDishes.NameMainDishes}&anh={mainDishes.Image}&gia={mainDishes.Price}&khoiluong={mainDishes.Mass}&mota={mainDishes.DescriptionMainDishes}&trangthai=1";
            var obj = JsonConvert.SerializeObject(mainDishes);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage Main = await client.PostAsync(url, content);
            if (Main.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllMainDishes", "Manage");
            }
            else
            {
                return RedirectToAction("AddMain", "Manage");
            }
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> UpdateMain(MainDishes mainDishes)
        {

            string urldetail = $"https://localhost:7031/api/ComboFastFood/GetMainDishesById?id={mainDishes.IDMainDishes}";
            var respon = await client.GetAsync(urldetail);
            var data = await respon.Content.ReadAsStringAsync();
            MainDishes main = JsonConvert.DeserializeObject<MainDishes>(data);

            string url = $"https://localhost:7031/api/ComboFastFood/UpdateMainDishes?id={mainDishes.IDMainDishes}&name={mainDishes.NameMainDishes}&anh={mainDishes.Image}&gia={mainDishes.Price}&khoiluong={mainDishes.Mass}&mota={mainDishes.DescriptionMainDishes}&trangthai={mainDishes.Status}";
            var obj = JsonConvert.SerializeObject(mainDishes);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage update = await client.PutAsync(url, content);
            if (update.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllMainDishes", "Manage");
            }
            return View(main);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMain(MainDishes mainDishes)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/DeleteMainDishes?id={mainDishes.IDMainDishes}";
            var obj = JsonConvert.SerializeObject(mainDishes);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage update = await client.PutAsync(url, content);
            return RedirectToAction("GetAllMainDishes", "Manage");
        }


        // Manage SideDishes

        [HttpGet]
        public async Task<IActionResult> GetAllSideDishes()
        {
            string url = $"https://localhost:7031/api/ComboFastFood/GetAllSidedishes";
            var respon = await client.GetAsync(url);
            var data = await respon.Content.ReadAsStringAsync();
            var sides = JsonConvert.DeserializeObject<List<SideDishes>>(data);
            return View(sides);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/GetSideDishesByid?id={id}";
            var respon = await client.GetAsync(url);
            var data = await respon.Content.ReadAsStringAsync();
            var sides = JsonConvert.DeserializeObject<SideDishes>(data);
            return View(sides);
        }
        [HttpGet]
        public async Task<IActionResult> AddSide()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSide(SideDishes sideDishes)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/CreateSideDishes?name={sideDishes.NameSideDishes}&image={sideDishes.Image}&mota={sideDishes.DescriptionSideDishes}&gia={sideDishes.Price}&khoiluong={sideDishes.Mass}&trangthai=1";
            var obj = JsonConvert.SerializeObject(sideDishes);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage side = await client.PostAsync(url, content);

            if (side.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSideDishes", "Manage");
            }
            else
            {
                return RedirectToAction("AddSide", "Manage");
            }


        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> UpdateSide(SideDishes sideDishes)
        {
            string urldtail = $"https://localhost:7031/api/ComboFastFood/GetSideDishesByid?id={sideDishes.IDSideDishes}";
            var respon = await client.GetAsync(urldtail);
            var data = await respon.Content.ReadAsStringAsync();
            var sides = JsonConvert.DeserializeObject<SideDishes>(data);

            string url = $"https://localhost:7031/api/ComboFastFood/UpdateDishes?id={sideDishes.IDSideDishes}&name={sideDishes.NameSideDishes}&image={sideDishes.Image}&mota={sideDishes.DescriptionSideDishes}&khoiluong={sideDishes.Mass}&trangthai={sideDishes.Status}";
            var obj = JsonConvert.SerializeObject(sideDishes);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage side = await client.PutAsync(url, content);
            if (side.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllSideDishes", "Manage");
            }
            else
            {
                return View(sides);
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteSide(SideDishes sideDishes)
        {
            string url = $"https://localhost:7031/api/ComboFastFood/DeleteSideDishes?id={sideDishes.IDSideDishes}";
            var obj = JsonConvert.SerializeObject(sideDishes);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage side = await client.PutAsync(url, content);

            return RedirectToAction("GetAllSideDishes", "Manage");

        }
    }
}
