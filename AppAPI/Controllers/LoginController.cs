using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		public ICustomerService _custom;
		public LoginController()
		{
			_custom = new CustomerSevice();
		}

		[HttpPost("[action]")]
		public bool Login(string email , string pass)
		{
			
			if(_custom.GetAllCus().Any(c => c.Email == email) == true)
			{
				if(_custom.GetAllCus().First(c => c.Email == email).Status == 1)
				{
                    if (_custom.GetAllCus().FirstOrDefault(c => c.Email == email).PassWord == pass)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                } else
				{
					return false;
				}
				
			}
			else
			{
				return false;
			}
		}
		
	}
}
