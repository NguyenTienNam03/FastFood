using AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace AppView.Areas.Admin.Controllers
{
    public class VoucherController : Controller
    {
        HttpClient _httpClient;
        public VoucherController()
        {
            _httpClient = new HttpClient();
        }
        // GET: VoucherController
        [HttpGet]
        public async Task<IActionResult> GetAllVoucher()
        {
            string url = $"https://localhost:7031/api/Voucher/GetAllVoucher";
            var repos = await _httpClient.GetAsync(url);
            var data = await repos.Content.ReadAsStringAsync();
            var voucher = JsonConvert.DeserializeObject<List<Voucher>>(data);
            return View(voucher);
        }

        // GET: VoucherController/Details/5
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> CreateVoucher(Voucher voucher)
        {
            string url = $"https://localhost:7031/api/Voucher/CreateVoucher?vouchercode={voucher.VoucherCode}&soluong={voucher.Quatity}&ngaybd={voucher.StartDate}&ngaykethuc={voucher.EndDate}&dieukien={voucher.Condition}&mota={voucher.Description}";
            var obj = JsonConvert.SerializeObject(voucher);
            StringContent content = new StringContent(obj , Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllVoucher", "Voucher");
            }
            else
            {
                return View();
            }
            
        }

        // GET: VoucherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VoucherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VoucherController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VoucherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VoucherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VoucherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
