using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace AppView.Areas.Customer.Controllers
{
    public class CustomerAccountController : Controller
    {
        private ICustomerService customerService;
        private ICartDetailService cartDetailService;
        private IDrinkService _drinkservice; 
        private IComboFastFoodService comboFastFoodservice;
        private ISideDishesService sideDishesService;
        private IMainDishesService mainDishesService;
        HttpClient client;
        public CustomerAccountController()
        {
            customerService = new CustomerSevice();
            _drinkservice = new DrinkService();
            cartDetailService = new CartDetailService();
            comboFastFoodservice = new ComboFastFoodService();
            sideDishesService = new SideDishesService();
            mainDishesService = new MainDishesService();
            client = new HttpClient();
        }
        [HttpGet]
        public async Task<IActionResult> Order()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;

            var user = HttpContext.User;
            var email = user.FindFirstValue(ClaimTypes.Email);
            var id = customerService.GetAllCus().FirstOrDefault(c => c.Email == email).IDCustomer;
            string url = $"https://localhost:7031/api/Cart/ShowCartDetail?id={id}";
            var repos = await client.GetAsync(url);
            var data = await repos.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<List<CartDetail>>(data);
            foreach (var item in cartDetailService.GetAllCartDetail(id))
            {
                var idfood = item.IDFood;
                if (_drinkservice.GetAllDrinks().Any(c => c.IDDrink == idfood))
                {
                    ViewBag.Image = _drinkservice.GetAllDrinks().FirstOrDefault(c => c.IDDrink == idfood).Image;
                    ViewBag.Name = _drinkservice.GetAllDrinks().FirstOrDefault(c => c.IDDrink == idfood).NameDrink;
                }
                else if (comboFastFoodservice.GetList().Any(c => c.IDCombo == idfood))
                {
                    ViewBag.Image = comboFastFoodservice.GetList().FirstOrDefault(c => c.IDCombo == idfood).Image;
                    ViewBag.Name = comboFastFoodservice.GetList().FirstOrDefault(c => c.IDCombo == idfood).NameCombo;
                }
                else if (sideDishesService.GetAllSideDishes().Any(c => c.IDSideDishes == idfood))
                {
                    ViewBag.Image = sideDishesService.GetAllSideDishes().FirstOrDefault(c => c.IDSideDishes == idfood).Image;
                    ViewBag.Name = sideDishesService.GetAllSideDishes().FirstOrDefault(c => c.IDSideDishes == idfood).NameSideDishes;
                }
                else if (mainDishesService.GetMainDishes().Any(c => c.IDMainDishes == idfood))
                {
                    ViewBag.Image = mainDishesService.GetMainDishes().FirstOrDefault(c => c.IDMainDishes == idfood).Image;
                    ViewBag.Name = mainDishesService.GetMainDishes().FirstOrDefault(c => c.IDMainDishes == idfood).NameMainDishes;
                }
            }

            return View(order);
        }

        public async Task<IActionResult> RemoveItem(CartDetail cartdetail)
        {
            string url = $"https://localhost:7031/api/Cart/DeleteCartDetail?id={cartdetail.IDCartDetail}";

            var obj = JsonConvert.SerializeObject(cartdetail) ;
            StringContent content = new StringContent(obj , Encoding.UTF8 , "application/json") ;
            HttpResponseMessage message = await client.DeleteAsync(url);

            return RedirectToAction("Order" , "CustomerAccount");
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> UpdateAmount(CartDetail cartDetail)
        {
            var user = HttpContext.User;
            var email = user.FindFirstValue(ClaimTypes.Email);
            var id = customerService.GetAllCus().FirstOrDefault(c => c.Email == email).IDCustomer;
            string url = $"https://localhost:7031/api/Cart/UpdateQuatity?id={id}&idcardetail={cartDetail.IDCartDetail}&quatity={cartDetail.Quatity}";
            var obj = JsonConvert.SerializeObject(cartDetail);
            StringContent content = new StringContent(obj , Encoding.UTF8 , "application/json") ;   
            HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Order", "CustomerAccount");
            } else
            {
                ViewBag.Error = "Cap nhat so luong that bai";
                return RedirectToAction("Order", "CustomerAccount");
            }
        }
    }
}
