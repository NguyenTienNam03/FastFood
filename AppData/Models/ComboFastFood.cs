using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class ComboFastFood
	{
		[Key]
		public Guid IDCombo { get ; set; }
		[ForeignKey(nameof(SideDishes))]
		public Guid? IDSideDishes { get ; set; }
		[ForeignKey(nameof(MainDishes))]
		public Guid? IDMainDishes { get ; set; }
		[ForeignKey(nameof(Drinks))]
		public Guid? IDDrink { get; set; }
		public string NameCombo { get; set; }
		public string Image { get; set; }
		public decimal Price { get; set; }
		public decimal PriceCombo { get; set; }
		public string DescriptionCombo { get; set; }
		public int Status { get; set; }

		public virtual SideDishes SideDishes { get; set; }
		public virtual Drinks Drinks { get; set; }
		public virtual MainDishes MainDishes { get; set; }

		public IEnumerable<CartDetail> cartDetails { get; set; }
		public IEnumerable<BillDetail> billDetails { get; set; }
	}
}
