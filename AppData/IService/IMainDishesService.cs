using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface IMainDishesService
	{
		public bool CreateMainDishes(MainDishes mainDishes);
		public bool UpdateMainDishes(MainDishes mainDishes);
		public bool DeleteMainDishes(Guid id);
		public List<MainDishes> GetMainDishes();
		public MainDishes GetMainDishesByID(Guid id);
		public MainDishes GetByName(string name);
	}
}
