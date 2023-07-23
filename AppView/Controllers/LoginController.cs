using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace AppView.Controllers
{
	public class LoginController : Controller
	{
		private IRoleService _roleser;
		private ICustomerService _customerService;
		HttpClient client = new HttpClient();

		public LoginController()
		{
			_roleser = new RoleService();
			_customerService = new CustomerSevice();

		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(Customer customer)
		{
			string url = $"https://localhost:7031/api/Login/Login?email={customer.Email}&pass={customer.PassWord}";
			var obj = JsonConvert.SerializeObject(customer);

			StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
			HttpResponseMessage login = await client.PostAsync(url, content);


			//
			//bool result = await login.Content.ReadAsAsync<bool>();
			List<Claim> claims = new List<Claim>()
					{
						new Claim(ClaimTypes.Email, customer.Email),
						new Claim("OtherProperties", "Example Role")
					};
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
				CookieAuthenticationDefaults.AuthenticationScheme);
			AuthenticationProperties properties = new AuthenticationProperties()
			{
				AllowRefresh = true,

			};
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
			new ClaimsPrincipal(claimsIdentity), properties);

			
			if(_customerService.GetAllCus().Any(c => c.Email == customer.Email) == false)
			{
				ViewBag.CheckEmail = "Ban chua co tai khoan. Hay dan ky";
				return View();
			}
			else
			{
                var idcus = _customerService.GetAllCus().FirstOrDefault(c => c.Email == customer.Email );
                var roleadmin = _roleser.GetAllRoles().FirstOrDefault(c => c.RoleName == "Admin");
                var rolecust = _roleser.GetAllRoles().FirstOrDefault(c => c.RoleName == "Customer");
				if(idcus.Status == 1)
				{
                    if (idcus.IDRole == roleadmin.IDRole && idcus.PassWord == customer.PassWord)
                    {
                        ViewBag.FullName = idcus.NameCustomer;
                        return RedirectToAction("Index", "ViewAccount", new { area = "Admin" });
                    }
                    else if (idcus.IDRole == rolecust.IDRole && idcus.PassWord == customer.PassWord)
                    {
                        ViewBag.FullName = idcus.NameCustomer;
                        return RedirectToAction("Index", "ViewCustomer", new { area = "Customer" });
                    }
                    else
                    {
                        ViewBag.ErrorLogin = "Dang nhap that bai. Ban kiem tra mat khau va email.";
                        return View();
                    }
                } else
				{
					ViewBag.TrangThai = "Tài Khoản của bạn đã dừng hoạt động. Bạn vui lòng kích hoạt lại bằng cách liên chúng tôi qua mail hoặc bạn có thể quên mật khẩu.";
					return View();
				}
                
            }
		}
		[HttpGet]
		public async Task<IActionResult> Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(AppData.Models.Customer customer)
		{
			string url = $"https://localhost:7031/api/Customer/Register?name={customer.NameCustomer}&phone={customer.PhoneNumber}&email={customer.Email}&pass={customer.PassWord}&city={customer.City}&district={customer.District}&address={customer.Address}";

			var obj = JsonConvert.SerializeObject(customer);
			StringContent conten = new StringContent(obj, Encoding.UTF8, "application/json");
			HttpResponseMessage register = await client.PostAsync(url, conten);
			if (register.IsSuccessStatusCode)
			{
				return RedirectToAction("Login", "Login");
			}
			else
			{
				return RedirectToAction("Register", "Login");
			}

		}
	}
}
