using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface IComboFastFoodService
	{
		public bool CreateCombo(ComboFastFood comboFastFood);
		public bool UpdateCombo(ComboFastFood comboFastFood);
		public bool DeleteCombo(Guid id);
		public List<ComboFastFood> GetList();
		public ComboFastFood GetComboFastFoodByID(Guid id);

	}
}
