using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class Role
	{
		[Key]
		public Guid IDRole { get; set; }
		public string RoleName { get; set; }
		public string RoleDescription { get; set; }
		public int Status { get; set; }
		public IEnumerable<Customer> Customers { get; set;}
	}
}
