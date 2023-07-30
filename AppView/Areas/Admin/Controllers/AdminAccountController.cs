using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        private ICustomerService customerService;
        private IPaymentService paymentService;
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
            paymentService = new PaymentService();
            cartDetailService = new CartDetailService();
            comboFastFoodservice = new ComboFastFoodService();
            sideDishesService = new SideDishesService();
            mainDishesService = new MainDishesService();
            client = new HttpClient();
        }
        public Guid IDCustomer()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var user = HttpContext.User;
            var email = user.FindFirstValue(ClaimTypes.Email);
            var id = customerService.GetAllCus().FirstOrDefault(c => c.Email == email).IDCustomer;
            return id;
        }
        [HttpGet]
        public async Task<IActionResult> Order()
        {
            var id = IDCustomer();
            string url = $"https://localhost:7031/api/Cart/ShowCartDetail?id={id}";
            var repos = await client.GetAsync(url);
            var data = await repos.Content.ReadAsStringAsync();
            var orderbyid = JsonConvert.DeserializeObject<List<CartDetail>>(data);
            return View(orderbyid);
        }
        [HttpGet]

        public async Task<IActionResult> ListBill()
        {
            var id = IDCustomer();
            string url = $"https://localhost:7031/api/Bill/GetAllBill?id={id}";
            var respos = await client.GetAsync(url);
            var data = await respos.Content.ReadAsStringAsync();
            var listbill = JsonConvert.DeserializeObject<List<Bill>>(data);
            return View(listbill);
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
            var id = IDCustomer();
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
        [HttpGet]
        [HttpPost]

        public async Task<IActionResult> Pay(Bill bill)
        {
            //Tao Bill
            var id = IDCustomer();
            var idpayment = paymentService.GetAllPayments().FirstOrDefault(c => c.Payment == "Thanh toan bang tien mat").IDPayment;
            string url = $"https://localhost:7031/api/Bill/CreateBill?idvoucher=5629e2e6-9fdf-4598-ab7d-2455079ea9b4&idcustom={id}&idpay={idpayment}";
            var obj = JsonConvert.SerializeObject(bill);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync(url, content);
            if (message.IsSuccessStatusCode)
            {
                return RedirectToAction("UpdateBill", "AdminAccount");
            } else
            {
                return RedirectToAction("Order", "AdminAccount");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Billdetail(Guid id)
        {
            string url = $"https://localhost:7031/api/Bill/GetBillDetailByIdbill?id={id}";
            var repos = await client.GetAsync(url);
            var data = await repos.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<BillDetail>>(data);
            return View(result);
        }

        [HttpGet]
        private Guid GetTheIdOfTheLatestBill()
        {
            var idcustomer = IDCustomer();
            string url = $"https://localhost:7031/api/Bill/Getthelatestvalue?idcustomer={idcustomer}";
            var repos = client.GetAsync(url).Result;
            var data =  repos.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Bill>(data);
            return result.IDBill;
        }
        [HttpGet]
        [HttpPut]
        public async Task<IActionResult> UpdateBill(Bill bill)
        {
            var IdBill = GetTheIdOfTheLatestBill();

            var note = bill.Note == null ? null : bill.Note;
            string urlUpdateBill = $"https://localhost:7031/api/Bill/UpdateBill?idbill={IdBill}&idvoucher={bill.IDVoucher}&idpay={bill.IDPayment}&name={bill.NameReceiver}&phone={bill.PhoneReceiver}&city={bill.CityReceiver}&distric={bill.DistrictReceiver}&note{note}";
            var obj = JsonConvert.SerializeObject(bill);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PutAsync(urlUpdateBill, content);
            if(message.IsSuccessStatusCode)
            {
                return RedirectToAction("ListBill", "AdminAccount");
            } else
            {
                string url = $"https://localhost:7031/api/Bill/GetBillById?id={IdBill}";
                var respon = await client.GetAsync(url);
                var data = await respon.Content.ReadAsStringAsync();
                var bill1 = JsonConvert.DeserializeObject<Bill>(data);
                return View(bill1);
            }


        }
    }
}
