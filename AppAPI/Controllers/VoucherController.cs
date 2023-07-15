using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VoucherController : ControllerBase
	{
		private IVoucherService _voucherService;
		public VoucherController()
		{
			_voucherService = new VoucherService();
		}
		// GET: api/<VoucherController>
		[HttpGet("[action]")]
		public IEnumerable<Voucher> GetAllVoucher()
		{
			return _voucherService.GetAllVouchers();
		}

		// GET api/<VoucherController>/5
		[HttpGet("[action]")]
		public Voucher GetByIDVoucher(Guid id)
		{
			return _voucherService.GetVoucherByID(id);
		}

		// POST api/<VoucherController>
		[HttpPost("[action]")]
		public bool CreateVoucher(string vouchercode , int soluong  , DateTime ngaybd , DateTime ngaykethuc, decimal dieukien, string mota )
		{
			if(_voucherService.GetAllVouchers().Any(c => c.VoucherCode == vouchercode && c.StartDate == ngaybd && c.EndDate == ngaykethuc))
			{
				Voucher voucher = new Voucher();
				voucher.IDVoucher = Guid.NewGuid();
				voucher.VoucherCode = vouchercode;
				voucher.Quatity = soluong;
				voucher.CreateDate = DateTime.Now;
				if (ngaybd.Date > DateTime.Now.Date && ngaykethuc.Date > ngaybd.Date)
				{
					voucher.StartDate = ngaybd;
					voucher.EndDate = ngaykethuc;
				}
				voucher.Condition = dieukien;
				voucher.Description = mota;
				voucher.Status = 1;
				return _voucherService.CreateVoucher(voucher);
			} else { return false; }
			
		}


		// PUT api/<VoucherController>/5
		[HttpPut("[action]")]
		public bool UpdateVoucher(Guid id, string vouchercode, int soluong, DateTime ngaybd, DateTime ngaykethuc, decimal dieukien, string mota, int trangthai)
		{
			if (_voucherService.GetAllVouchers().Any(c => c.VoucherCode == vouchercode && c.StartDate == ngaybd && c.EndDate == ngaykethuc))
			{
				Voucher voucher = _voucherService.GetAllVouchers().FirstOrDefault(c => c.IDVoucher == id);
				voucher.IDVoucher = Guid.NewGuid();
				voucher.VoucherCode = vouchercode;
				voucher.Quatity = soluong;
				voucher.CreateDate = DateTime.Now;
				if (ngaybd.Date > DateTime.Now.Date && ngaykethuc.Date > ngaybd.Date)
				{
					voucher.StartDate = ngaybd;
					voucher.EndDate = ngaykethuc;
				}
				voucher.Condition = dieukien;
				voucher.Description = mota;
				voucher.Status = trangthai;
				return _voucherService.CreateVoucher(voucher);
			}
			else { return false; }
		}

		// DELETE api/<VoucherController>/5
		[HttpDelete("[action]")]
		public bool DeleteVoucher(Guid id)
		{
			return _voucherService.DeleteVoucher(id);
		}
	}
}
