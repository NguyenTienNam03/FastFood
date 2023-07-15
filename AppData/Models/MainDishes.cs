﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class MainDishes
	{
		[Key]
		public Guid IDMainDishes { get; set; }
		[MaxLength(225)]
		public string NameMainDishes { get; set; }
		public string Image { get; set; }

		[Range(0 , int.MaxValue , ErrorMessage = "Gia lon hon 0.")]
		public decimal Price { get; set; }
		[Range(100, int.MaxValue, ErrorMessage = "Khoi luong lon hon 100.")]
		public decimal Mass { get; set; } // Don vi la gam
		public string DescriptionMainDishes { get; set; }
		public int Status { get; set; }
		public IEnumerable<ComboFastFood> ComboFastFood { get; set; }
		public IEnumerable<CartDetail> CartDetail { get; set; }
		public IEnumerable<BillDetail> BillDetail { get; set; }
	}
}
