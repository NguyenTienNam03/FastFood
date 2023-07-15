using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class Cart
	{
		[Key]
		[ForeignKey(nameof(Customer))]
		public Guid IDCart { get; set; }
		public string Description { get; set; }

		public virtual Customer Customer { get; set; }
		public IEnumerable<CartDetail> Details { get; set; }
	}
}
