using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class BillDetail
	{
		[Key]
		public Guid IDBillDetail { get; set; }
		[ForeignKey(nameof(Bill))]
		public Guid IDBill { get; set; }
		public Guid IDFood { get ; set; }
		public int Quatity { get; set; }
		public decimal Price { get; set; }

		//public virtual Bill Bill { get; set; }
		//public virtual MainDishes MainDishes { get; set; }
		//public virtual SideDishes SideDishes { get; set; }
		//public virtual ComboFastFood ComboFastFood { get; set; }
		//public virtual Drinks Drinks { get; set; }

	}
}
