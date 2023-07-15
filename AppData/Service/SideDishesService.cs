using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class SideDishesService : ISideDishesService
	{
		private DB_Context _context;
		public SideDishesService()
		{
			_context = new DB_Context();
		}
		public bool CreateSideDishes(SideDishes sideDishes)
		{
			try
			{
				_context.sideDishes.Add(sideDishes);
				_context.SaveChanges();
				return true;
			}catch { return false; }
		}

		public bool DeleteSideDishes(Guid id)
		{
			try
			{
				var side = _context.sideDishes.FirstOrDefault(c => c.IDSideDishes == id);
				if (side != null)
				{
					side.Status = 0;
					_context.sideDishes.Update(side);
					_context.SaveChanges();
					return true;
				}
				return true;
			}
			catch { return false; }
		}

		public List<SideDishes> GetAllSideDishes()
		{
			return _context.sideDishes.ToList();
		}

		public SideDishes GetSideDishesByID(Guid id)
		{
			return _context.sideDishes.FirstOrDefault(c => c.IDSideDishes == id);
		}

		public SideDishes GetSideDishesByName(string name)
		{
			return _context.sideDishes.FirstOrDefault(c => c.NameSideDishes.Contains(name));	
		}

		public bool UpdateSideDishes(SideDishes sideDishes)
		{
			try
			{
				var side = _context.sideDishes.FirstOrDefault(c => c.IDSideDishes == sideDishes.IDSideDishes);
				if (side != null)
				{
					side.NameSideDishes = sideDishes.NameSideDishes;
					side.Price = sideDishes.Price;
					side.Mass = sideDishes.Mass;
					side.Image = sideDishes.Image;
					side.Status = sideDishes.Status;
					side.DescriptionSideDishes = sideDishes.DescriptionSideDishes;
					_context.sideDishes.Update(side);
					_context.SaveChanges();
				}
				return true;
			}
			catch { return false; }
		}
	}
}
