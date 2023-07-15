using AppData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModels
{
	public class ComboFastFoodViewModel
	{
		public Guid IDCombo { get; set; }
		public string? SideDishes { get; set; }
		public string? MainDishes { get; set; }
		public string? Drink { get; set; }
		public string NameCombo { get; set; }
		public string Image { get; set; }
		public decimal Price { get; set; }
		public decimal PriceCombo { get; set; }
		public string DescriptionCombo { get; set; }
		public int Status { get; set; }
	}
}
