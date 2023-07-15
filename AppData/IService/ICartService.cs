using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface ICartService
	{
		public bool CreateCart(Cart cart);
		public List<Cart> GetAllCart();
	}
}
