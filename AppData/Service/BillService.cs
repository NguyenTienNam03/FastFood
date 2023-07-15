using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class BillService : IBillService
	{
		private DB_Context _context;
		public BillService()
		{
			_context = new DB_Context();
		}
		public bool CreateBill(Bill bill)
		{
			try
			{
				_context.bills.Add(bill);
				_context.SaveChanges();
				return true;
			}
			catch 
			{
				return false;	
			}
		}

		public bool DeleteBill(Guid IDbill)
		{
			try
			{
				return true;
			}
			catch
			{
				return false;
			}
		}

		public List<Bill> GetBillList()
		{
			return _context.bills.ToList();
		}

		public Bill GetByID(Guid IDbill)
		{
			return _context.bills.FirstOrDefault(c => c.IDBill == IDbill);
		}

		public bool UpdateBill(Bill bill)
		{
			try
			{
				var bill1 = _context.bills.FirstOrDefault(c => c.IDBill == bill.IDBill);
				bill1.IDVoucher = bill.IDVoucher;
				bill1.Quatity = bill.Quatity;
				bill1.TotalPayment = bill.TotalPayment;
				bill1.TotalAmount = bill.TotalAmount;
				bill1.Note = bill.Note;
				bill1.Status = bill.Status;
				_context.bills.Update(bill1);
				_context.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
