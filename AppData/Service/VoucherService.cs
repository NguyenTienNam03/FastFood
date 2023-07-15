using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class VoucherService : IVoucherService
	{
		private DB_Context _context;
		public VoucherService() 
		{
			_context = new DB_Context();
		}
		public bool CreateVoucher(Voucher voucher)
		{
			try
			{
				_context.vouchers.Add(voucher);
				_context.SaveChanges();
				return true;
			}catch (Exception ex)
			{
				return false;
			}
		}

		public bool DeleteVoucher(Guid idvh)
		{
			try
			{
				var voucher = _context.vouchers.FirstOrDefault(c => c.IDVoucher == idvh);
				if (voucher != null)
				{
					voucher.Status = 0;
					_context.vouchers.Update(voucher);
					_context.SaveChanges();
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public List<Voucher> GetAllVouchers()
		{
			return _context.vouchers.ToList();
		}

		public Voucher GetVoucherByID(Guid idvh)
		{
			return _context.vouchers.FirstOrDefault(c => c.IDVoucher == idvh);
		}

		public bool UpdateVoucher(Voucher voucher)
		{
			try
			{
				var voucher1 = _context.vouchers.FirstOrDefault(c => c.IDVoucher == voucher.IDVoucher);
				if (voucher1 != null)
				{
					voucher1.Quatity = voucher.Quatity;
					voucher1.Status = voucher.Status;
					voucher1.StartDate = voucher.StartDate;
					voucher1.EndDate = voucher.EndDate;
					voucher1.Condition = voucher.Condition;
					voucher1.Description = voucher.Description;
					_context.vouchers.Update(voucher1);
					_context.SaveChanges();
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
