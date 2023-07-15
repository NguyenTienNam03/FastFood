using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class Bill
	{
		[Key]
		public Guid IDBill { get ; set; }
		[ForeignKey(nameof(Voucher))]
		public Guid IDVoucher { get ; set; }
		[ForeignKey(nameof(Customer))]
		public Guid IDCustomer { get; set; }
		[ForeignKey(nameof(Payments))]
		public Guid IDPayment { get; set; }
		public string InvoiceCode { get ; set; }
		public string Quatity { get ; set; }
		public string NameReceiver { get ; set; }
		public string PhoneReceiver { get ; set; }
		public string CityReceiver { get; set; }
		public string DistrictReceiver { get; set; }
		public decimal TotalAmount { get ; set; }
		public decimal TransportFee { get ; set; }
		public decimal TotalPayment { get ; set; }
		public DateTime OrderDate { get ; set; }
		public DateTime DeliveryDate { get ; set; }
		public DateTime DateOfPayment { get ; set; }
		public string? Note { get ; set; }
		public int Status { get ; set; }
		public virtual Voucher Voucher { get ; set; }
		public virtual Payments Payments { get ; set; }
		public virtual Customer Customer { get; set; }
		
		public IEnumerable<BillDetail> Details { get ; set; }

	}
}
