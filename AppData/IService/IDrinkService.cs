using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface IDrinkService
	{
		public bool CreateDrink(Drinks drinks);
		public bool UpdateDrink(Drinks drinks);
		public bool DeleteDrink(Guid id);
		public List<Drinks> GetAllDrinks();
		public Drinks GetDrinkById(Guid id);
		public Drinks GetDrinkByName(string name);
	}
}
