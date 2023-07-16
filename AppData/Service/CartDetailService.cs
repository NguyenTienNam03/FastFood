using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class CartDetailService : ICartDetailService
	{
		private DB_Context _context;

		public CartDetailService()
		{
			_context = new DB_Context();
		}
		public bool CreateCartDetail(CartDetail cartdetail)
		{
			try
			{
				_context.cartDetails.Add(cartdetail);
				_context.SaveChanges();
				return true;
			} catch (Exception ex)
			{
				return false;
			}
		}

		public bool DeleteCartDetail(Guid IDcartDetail)
		{
			try
			{
				var id = _context.cartDetails.FirstOrDefault(c => c.IDCartDetail == IDcartDetail);
				if (id != null)
				{
					_context.cartDetails.Remove(id);
					_context.SaveChanges();
				}
				return true;
			} catch(Exception ex)
			{
				return false;
			}
		}

		public List<CartDetail> GetAllCartDetail(Guid id)
		{
			return _context.cartDetails.Where(c => c.IDCart == id).ToList();
		}

		public bool UpdateCartDetail(CartDetail cartdetail)
		{
			try
			{
				var id = _context.cartDetails.FirstOrDefault(c => c.IDCartDetail == cartdetail.IDCartDetail);
				if (id != null)
				{
					id.Quatity = cartdetail.Quatity;
					_context.cartDetails.Update(id);
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
