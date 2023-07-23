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
            string url = $"https://localhost:7031/api/Payments/CreatPayment?Payment={payments.Payment}&mota={payments.Description}&trangthai=1";
            var obj = JsonConvert.SerializeObject(payments);
            StringContent content = new StringContent(obj, Encoding.UTF8 , "application/json");
            HttpResponseMessage message = client.PostAsync(url, content).Result;
            return true;
        }
        private bool Transfer(Payments payments)
        {
            string url = $"https://localhost:7031/api/Payments/CreatPayment?Payment={payments.Payment}&mota={payments.Description}&bankaccount={payments.Bankaccount}&bankname={payments.BankName}&Imageqr={payments.ImageQR}&trangthai=1";
            var obj = JsonConvert.SerializeObject(payments);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PostAsync(url, content).Result;
            return true;
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> UpdatePayment(Payments payments)
        {
            if (payments.BankName == null && payments.BankAccountNumber == null && payments.Bankaccount == null && payments.ImageQR == null)
            {
                UpdateThanhToanTienMat(payments);
                return RedirectToAction("GetAllPayment", "Payment");
            }
            else if (payments.BankName != null && payments.BankAccountNumber != null && payments.Bankaccount != null && payments.ImageQR != null)
            {
                UpdateTransfer(payments);
                return RedirectToAction("GetAllPayment", "Payment");
            }
            else
            {
                string url = $"https://localhost:7031/api/Payments/GetByID?id={payments.IDPayment}";
                var respos = await client.GetAsync(url);
                var data = await respos.Content.ReadAsStringAsync();
                var payment = JsonConvert.DeserializeObject<Payments>(data);
                return View(payment);
            }

        }
        private bool UpdateThanhToanTienMat(Payments payments)
        {
            string url = $"https://localhost:7031/api/Payments/UpdatePayment?id={payments.IDPayment}&Payment={payments.Payment}&mota={payments.Description}&trangthai={payments.Status}";
            var obj = JsonConvert.SerializeObject(payments);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PutAsync(url, content).Result;
            return true;
        }
        private bool UpdateTransfer(Payments payments)
        {
            string url = $"https://localhost:7031/api/Payments/UpdatePayment?id={payments.IDPayment}&Payment={payments.Payment}&mota={payments.Description}&bankaccount={payments.Bankaccount}&banknumber={payments.BankAccountNumber}&bankname={payments.BankName}&Imageqr={payments.ImageQR}&trangthai={payments.Status}";
            var obj = JsonConvert.SerializeObject(payments);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PutAsync(url, content).Result;
            return true;
        }
        [HttpGet]
        [HttpPut]
        public async Task<IActionResult> Deletepayment(Payments payments)
        {
            string url = $"https://localhost:7031/api/Role/DeleteRole?id={payments.IDPayment}";
            var obj = JsonConvert.SerializeObject(payments);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PutAsync(url, content).Result;
            return RedirectToAction("GetAllPayment", "Payment");
        }
    }
}
