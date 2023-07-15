using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class MainDishesService : IMainDishesService
	{
		private DB_Context _context;
		public MainDishesService()
		{
			_context = new DB_Context();
		}
		public bool CreateMainDishes(MainDishes mainDishes)
		{
			try
			{
				_context.mainDishes.Add(mainDishes);
				_context.SaveChanges();
				return true;
			} catch (Exception ex)
			{
				return false;
			}
		}

		public bool DeleteMainDishes(Guid id)
		{
			try
			{
				var main = _context.mainDishes.FirstOrDefault(c => c.IDMainDishes  == id);
				if (main != null)
				{
					main.Status = 0;
					_context.mainDishes.Update(main);
					_context.SaveChanges();
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public MainDishes GetByName(string name)
		{
			return _context.mainDishes.FirstOrDefault(c => c.NameMainDishes.Contains(name));
		}

		public List<MainDishes> GetMainDishes()
		{
			return _context.mainDishes.ToList();
		}

		public MainDishes GetMainDishesByID(Guid id)
		{
			return _context.mainDishes.FirstOrDefault(c => c.IDMainDishes == id);
		}

		public bool UpdateMainDishes(MainDishes mainDishes)
		{
			try
			{
				var main = _context.mainDishes.FirstOrDefault(c => c.IDMainDishes == mainDishes.IDMainDishes);
				if (main != null)
				{
					main.NameMainDishes = mainDishes.NameMainDishes;
					main.Price = mainDishes.Price;
					main.Status = mainDishes.Status;
					main.Image = mainDishes.Image;
					main.Mass = mainDishes.Mass;
					main.DescriptionMainDishes = mainDishes.DescriptionMainDishes;
					_context.mainDishes.Update(main);
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
