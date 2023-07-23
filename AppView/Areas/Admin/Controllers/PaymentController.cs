using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class PaymentController : Controller
    {
        HttpClient client = new HttpClient();

        public PaymentController()
        {

        }


        [HttpGet] 
        public async Task<IActionResult> GetAllPayment()
        {
            string url = $"https://localhost:7031/api/Payments/GetAllPayment";
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            var dataapi = JsonConvert.DeserializeObject<List<Payments>>(data); 
            return View(dataapi);
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> AddPayment(Payments payments)
        {
            if(payments.BankName == null && payments.BankAccountNumber == null && payments.Bankaccount == null && payments.ImageQR == null)
            {
                Thanhtoantienmat(payments);
                return RedirectToAction("GetAllPayment", "Payment");
            } else if(payments.BankName != null && payments.BankAccountNumber != null && payments.Bankaccount != null && payments.ImageQR != null)
            {
                Transfer(payments);
                return RedirectToAction("GetAllPayment", "Payment");
            } else
            {
                return View();
            }
        }

        private bool Thanhtoantienmat(Payments payments)
        {
            string url = $"https://localhost:7031/api/Payments/CreatPayment?Payment={payments.Payment}&mota={payments.Description}";
            var obj = JsonConvert.SerializeObject(payments);
            StringContent content = new StringContent(obj, Encoding.UTF8 , "application/json");
            HttpResponseMessage message = client.PostAsync(url, content).Result;
            return true;
        }
        private bool Transfer(Payments payments)
        {
            string url = $"https://localhost:7031/api/Payments/CreatPayment?Payment={payments.Payment}&mota={payments.Description}&bankaccount={payments.Bankaccount}&bankname={payments.BankName}&Imageqr={payments.ImageQR}";
            var obj = JsonConvert.SerializeObject(payments);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PostAsync(url, content).Result;
            return true;
        }

        private bool UpdateThanhToanTienMat(Payments payments)
        {
            return true;
        }
    }
}
