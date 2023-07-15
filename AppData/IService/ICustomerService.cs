using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface ICustomerService
	{
		public bool CreateCustomer(Customer customer);
		public bool UpdateCustomer(Customer customer);
		public bool DeleteCustomer(Guid idcus);
		public List<Customer> GetAllCus();
		public Customer GetByID(Guid id);
		public Customer GetByName(string name);
	}
}
