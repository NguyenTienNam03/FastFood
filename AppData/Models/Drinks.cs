using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class Drinks
	{
		[Key]
		public Guid IDDrink { get; set; }
		[MaxLength(225)]	
		public string NameDrink { get; set; }
		public string Image { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "Gia lon hon 0.")]
		public decimal Price { get; set; }
		[Range(100 , int.MaxValue , ErrorMessage = "Khoi luong lon hon 100.")]
		public decimal Mass { get; set; } // don vi la mm
		public int Status { get; set; }
		public IEnumerable<ComboFastFood> ComboFastFood { get; set; }
		public IEnumerable<CartDetail> CartDetail { get; set; }
		public IEnumerable<BillDetail> BillDetail { get; set; }
	}
}
