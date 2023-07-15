using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class CustomerSevice : ICustomerService
	{
		private DB_Context _context;

		public CustomerSevice()
		{
			_context = new DB_Context();
		}
		public bool CreateCustomer(Customer customer)
		{
			try
			{
				_context.customers.Add(customer);
				_context.SaveChanges();
				return true;
			} catch { return false; }
		}

		public bool DeleteCustomer(Guid idcus)
		{
			try
			{
				var id = _context.customers.FirstOrDefault(c => c.IDCustomer == idcus);
				if (id != null)
				{
					id.Status = 0;
					_context.customers.Update(id);
					_context.SaveChanges();
				}
				return true;
			}
			catch { return false; }
		}

		public List<Customer> GetAllCus()
		{
			return _context.customers.ToList();
		}

		public Customer GetByID(Guid id)
		{
			return _context.customers.FirstOrDefault(c => c.IDCustomer == id);
		}

		public Customer GetByName(string name)
		{
			return _context.customers.FirstOrDefault(c => c.NameCustomer.Contains(name));
		}

		public bool UpdateCustomer(Customer customer)
		{
			try
			{
				var cus = _context.customers.FirstOrDefault(c => c.IDCustomer == customer.IDCustomer);
				if (cus != null)
				{
					cus.NameCustomer = customer.NameCustomer;
					cus.PhoneNumber = customer.PhoneNumber;
					cus.City = customer.City;
					cus.Email = customer.Email;
					cus.PassWord = customer.PassWord;
					cus.Address = customer.Address;
					cus.District = customer.District;
					cus.Status = customer.Status;
					_context.customers.Update(cus);
					_context.SaveChanges();
				}
				return true;
			}
			catch { return false; }
		}
	}
}
