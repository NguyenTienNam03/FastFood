using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private ICustomerService _customerService;
		private IRoleService _roleservice;
		public CustomerController()
		{
			_customerService = new CustomerSevice();
			_roleservice = new RoleService();
		}
		// GET: api/<CustomerController>
		[HttpGet("[action]")]
		public IEnumerable<Customer> GetAllcus()
		{
			return _customerService.GetAllCus();
		}

		// GET api/<CustomerController>/5
		[HttpGet("[action]")]
		public Customer GetById(Guid id)
		{
			return _customerService.GetByID(id);
		}
		[HttpGet("[action]")]
		public Customer GetByName(string name)
		{
			return _customerService.GetByName(name);
		}

		// POST api/<CustomerController>
		[HttpPost("[action]")]
		public bool AddCus(Guid idrole, string name , string phone, string email, string pass, string city, string district, string address)
		{
			if(_customerService.GetAllCus().Any(c => c.Email == email || c.PhoneNumber == phone) == false)
			{
				Customer customer = new Customer();
				customer.IDCustomer = Guid.NewGuid();
				customer.IDRole = _roleservice.GetAllRoles().FirstOrDefault(c => c.IDRole == idrole).IDRole;
				customer.NameCustomer = name;
				customer.PhoneNumber = phone;
				customer.Email = email;
				customer.PassWord = pass;
				customer.City = city;
				customer.District = district;
				customer.Address = address;
				customer.Status = 1;
				return _customerService.CreateCustomer(customer);
			} else
			{
				return false;
			}
		
		}
		[HttpPost("[action]")]
		public bool Register(string name, string phone, string email, string pass, string city, string district, string address)
		{
			if (_customerService.GetAllCus().Any(c => c.Email == email || c.PhoneNumber == phone) == false)
			{

				Customer customer = new Customer();
				customer.IDCustomer = Guid.NewGuid();
				customer.NameCustomer = name;
				customer.PhoneNumber = phone;
				customer.Email = email;
				customer.PassWord = pass;
				customer.City = city;
				customer.District = district;
				customer.Address = address;
				customer.Status = 1;
				customer.IDRole = _roleservice.GetAllRoles().First(c => c.RoleName == "Customer").IDRole;
				return _customerService.CreateCustomer(customer);
			}
			else
			{
				return false;
			}
		}
		// PUT api/<CustomerController>/5
		[HttpPut("[action]")]
		public bool CusUpdateInfo(Guid id, string name, string phone, string email, string pass, string city, string district, string address)
		{
			
			if(_customerService.GetAllCus().Any(c => c.Email == email || c.PhoneNumber == phone) == false)
			{
				Customer customer = _customerService.GetAllCus().FirstOrDefault(c => c.IDCustomer == id);
				customer.NameCustomer = name;
				customer.PhoneNumber = phone;
				customer.Email = email;
				customer.PassWord = pass;
				customer.City = city;
				customer.District = district;
				customer.Address = address;
				customer.Status = 1;
				customer.IDRole = _roleservice.GetAllRoles().First(c => c.RoleName == "Customer").IDRole; ;
				return _customerService.UpdateCustomer(customer);
			} else
			{
				return false;
			}
			
		}

		// DELETE api/<CustomerController>/5
		[HttpPut("[action]")]
		public bool DeleteCus(Guid id)
		{
			return _customerService.DeleteCustomer(id);
		}
	}
}
