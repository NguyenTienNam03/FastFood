using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class BillDetailService : IBillDetailService
	{
		private DB_Context _context;
		public BillDetailService()
		{
			_context = new DB_Context();
		}
		public bool CreateBillDetail(BillDetail billDetail)
		{
			try
			{
				_context.billDetails.Add(billDetail);
				_context.SaveChanges();
				return true;
			} catch (Exception ex)
			{
				return false;
			}
		}

        public List<BillDetail> GetBillDetailList(Guid idbill)
        {
            return _context.billDetails.Where(c => c.IDBill == idbill).ToList();
        }
    }
}
