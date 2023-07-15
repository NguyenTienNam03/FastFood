using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface IBillService
	{
		public bool CreateBill(Bill bill);
		public bool UpdateBill(Bill bill);
		public bool DeleteBill(Guid IDbill);
		public List<Bill> GetBillList();
		public Bill GetByID(Guid IDbill);
	}
}
