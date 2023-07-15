using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface ICartDetailService
	{
		public bool CreateCartDetail(CartDetail cartdetail);
		public bool UpdateCartDetail(CartDetail cartdetail);
		public bool DeleteCartDetail(Guid IDcartDetail);
		public List<CartDetail> GetAllCartDetail(Guid? id);
	}
}
