using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class DrinkService : IDrinkService
	{
		private DB_Context _context;
		public DrinkService()
		{
			_context = new DB_Context();
		}
		public bool CreateDrink(Drinks drinks)
		{
			try
			{
				_context.drinks.Add(drinks);
				_context.SaveChanges();
				return true;
			}catch (Exception ex)
			{
				return false;
			}
		}

		public bool DeleteDrink(Guid id)
		{
			try
			{
				var drink = _context.drinks.FirstOrDefault(c => c.IDDrink == id);
				if (drink != null)
				{
					drink.Status = 0;
					_context.drinks.Update(drink);
					_context.SaveChanges();
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public List<Drinks> GetAllDrinks()
		{
			return _context.drinks.ToList();
		}

		public Drinks GetDrinkById(Guid id)
		{
			return _context.drinks.FirstOrDefault(c => c.IDDrink == id);
		}

		public Drinks GetDrinkByName(string name)
		{
			return _context.drinks.FirstOrDefault(c => c.NameDrink.Contains(name));
		}

		public bool UpdateDrink(Drinks drinks)
		{
			try
			{
				var drink = _context.drinks.FirstOrDefault(c => c.IDDrink == drinks.IDDrink);
				if (drink != null)
				{
					drink.NameDrink = drinks.NameDrink;
					drink.Price = drinks.Price;
					drink.Image = drinks.Image;
					drink.Mass = drinks.Mass;
					drink.Status = drinks.Status;
					_context.drinks.Update(drink);
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
