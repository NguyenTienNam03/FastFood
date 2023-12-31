﻿using AppData.IService;
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
        private IPaymentService paymentService;
        HttpClient client;
        public CustomerAccountController()
        {
            customerService = new CustomerSevice();
            _drinkservice = new DrinkService();
            cartDetailService = new CartDetailService();
            comboFastFoodservice = new ComboFastFoodService();
            sideDishesService = new SideDishesService();
            mainDishesService = new MainDishesService();
            paymentService = new PaymentService();
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
        [HttpGet]
        [HttpPost]

        public async Task<IActionResult> Pay(Bill bill)
        {
            //Tao Bill
            var user = HttpContext.User;
            var email = user.FindFirstValue(ClaimTypes.Email);
            var iduser = customerService.GetAllCus().FirstOrDefault(c => c.Email == email).IDCustomer;
            var idpayment = paymentService.GetAllPayments().FirstOrDefault(c => c.Payment == "Thanh toan bang tien mat").IDPayment;
            string url = $"https://localhost:7031/api/Bill/CreateBill?idvoucher=5629e2e6-9fdf-4598-ab7d-2455079ea9b4&idcustom={iduser}&idpay={idpayment}";
            var obj = JsonConvert.SerializeObject(bill);
            StringContent content = new StringContent(obj , Encoding.UTF8 , "application/json");
            HttpResponseMessage message = await client.PostAsync(url, content);
            if(message.IsSuccessStatusCode)
            {
                return RedirectToAction();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> BillDetail(Guid id)
        {
            string url = $"";
            return View();
        }
        [HttpGet]
        [HttpPut]
        public async Task<IActionResult> UpdateBill()
        {
            return View();
        }
    }
}
