using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class Payments
	{
		[Key]
		public Guid IDPayment { get; set; }
		[Required]
		public string Payment { get; set; }
		[Required]
		public string Description { get; set; }
		public string? Bankaccount { get; set; }
		public string? BankAccountNumber { get; set; }
		public string? BankName { get; set; }
		public string? ImageQR { get; set; }
		[Required]
		public int Status { get; set; }

		public IEnumerable<Bill> bills { get; set; }
	}
}
