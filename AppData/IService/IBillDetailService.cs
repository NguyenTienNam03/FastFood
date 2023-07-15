using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface IBillDetailService
	{
		public bool CreateBillDetail(BillDetail billDetail);

		public List<BillDetail> GetBillDetailList(Guid idbill);

	}
}
