using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class Customer
	{
		[Key]
		public Guid IDCustomer { get; set; }
		[ForeignKey(nameof(Role))]
		public Guid IDRole { get; set; }
		public string NameCustomer { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string PassWord { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string Address { get; set; }
		public int Status { get; set; }

		public virtual Role Role { get; set; }
		public IEnumerable<Cart> carts { get; set; }
		public IEnumerable<Bill> bills { get; set; }
	}
}
