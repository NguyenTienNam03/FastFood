using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface ISideDishesService
	{
		public bool CreateSideDishes(SideDishes sideDishes);
		public bool DeleteSideDishes(Guid id);
		public bool UpdateSideDishes(SideDishes sideDishes);
		public List<SideDishes> GetAllSideDishes();
		public SideDishes GetSideDishesByID(Guid id);
		public SideDishes GetSideDishesByName(string name);

	}
}
