using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class Voucher
	{
		[Key]
		public Guid IDVoucher { get ; set; }
		public string VoucherCode { get; set; }
		public int Quatity { get; set; }
		public decimal VoucherValue { get; set; }
        public DateTime CreateDate { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public decimal Condition { get; set; }
		public string Description { get; set; }
		public int Status { get; set; }
		
		public IEnumerable<Bill> Bill { get; set; }
	}
}
