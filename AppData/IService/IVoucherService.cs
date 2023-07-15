using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface IVoucherService
	{
		public bool CreateVoucher(Voucher voucher);
		public bool UpdateVoucher(Voucher voucher);
		public bool DeleteVoucher(Guid idvh);
		public List<Voucher> GetAllVouchers();
		public Voucher GetVoucherByID(Guid idvh);
	}
}
