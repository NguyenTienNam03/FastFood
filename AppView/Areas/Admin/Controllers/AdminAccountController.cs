﻿using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        private ICustomerService customerService;

        private ICartDetailService cartDetailService;
        private IDrinkService _drinkservice;
        private IComboFastFoodService comboFastFoodservice;
        private ISideDishesService sideDishesService;
        private IMainDishesService mainDishesService;
        HttpClient client;
        public AdminAccountController()
        {
            customerService = new CustomerSevice();
            _drinkservice = new DrinkService();
            cartDetailService = new CartDetailService();
            comboFastFoodservice = new ComboFastFoodService();
            sideDishesService = new SideDishesService();
            mainDishesService = new MainDishesService();
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
            return View(orderbyid);
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> RemoveItem(CartDetail cartdetail)
        {
            string url = $"https://localhost:7031/api/Cart/DeleteCartDetail?id={cartdetail.IDCartDetail}";

            var obj = JsonConvert.SerializeObject(cartdetail);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.DeleteAsync(url);

            return RedirectToAction("Order", "AdminAccount");
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
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Order", "AdminAccount");
            }
            else
            {
                ViewBag.Error = "Cap nhat so luong that bai";
                return RedirectToAction("Order", "AdminAccount");
            }
        }
    }
}
