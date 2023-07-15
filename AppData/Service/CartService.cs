using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class CartService : ICartService
	{
		private DB_Context _context;
		public CartService()
		{
			_context = new DB_Context();
		}

		public bool CreateCart(Cart cart)
		{
			try
			{
				_context.carts.Add(cart);
				_context.SaveChanges();
				return true;
			} catch { return false; }
		}

        public List<Cart> GetAllCart()
        {
            return _context.carts.ToList();
        }
    }
}
