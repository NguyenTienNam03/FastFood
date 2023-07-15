using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class ComboFastFoodService : IComboFastFoodService
	{
		private DB_Context _context;
		public ComboFastFoodService()
		{
			_context = new DB_Context();
		}

		public bool CreateCombo(ComboFastFood comboFastFood)
		{
			try
			{
				_context.comboFastFoods.Add(comboFastFood);
				_context.SaveChanges();
				return true;
			} catch { return false; }
		}

		public bool DeleteCombo(Guid id)
		{
			try
			{
				var id1 = _context.comboFastFoods.FirstOrDefault(x => x.IDCombo == id);
				if (id1 != null)
				{
					id1.Status = 0;
					_context.comboFastFoods.Update(id1);
					_context.SaveChanges();
				}
				return true;
			}
			catch { return false; }
		}

		public ComboFastFood GetComboFastFoodByID(Guid id)
		{
			return _context.comboFastFoods.FirstOrDefault(c => c.IDCombo == id);
		}

		public List<ComboFastFood> GetList()
		{
			return _context.comboFastFoods.ToList();
		}

		public bool UpdateCombo(ComboFastFood comboFastFood)
		{
			try
			{
				var combo = _context.comboFastFoods.FirstOrDefault(c => c.IDCombo == comboFastFood.IDCombo);
				if (combo != null)
				{
					combo.IDDrink = comboFastFood.IDDrink;
					combo.IDMainDishes = comboFastFood.IDMainDishes;
					combo.IDSideDishes = comboFastFood.IDSideDishes;
					combo.NameCombo = comboFastFood.NameCombo;
					combo.Image = comboFastFood.Image;
					combo.Price = comboFastFood.Price;
					combo.PriceCombo = comboFastFood.PriceCombo;
					combo.DescriptionCombo = comboFastFood.DescriptionCombo;
					combo.Status = comboFastFood.Status;
					_context.comboFastFoods.Update(combo);
					_context.SaveChanges();
				}
				return true;
			}
			catch { return false; }
		}
	}
}
