using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class CartDetail
	{
		[Key]
		public Guid IDCartDetail { get; set; }
		[ForeignKey(nameof(Cart))]
		public Guid IDCart { get; set; }
        [ForeignKey(nameof(Setkey))]
        public Guid IDsetkey { get; set; }
		public Guid IDFood { get; set; }
		public int Quatity { get; set; }
		public decimal Price { get; set; }

		public virtual Cart Cart { get; set; }
		public virtual Setkey Setkey { get; set; }
	}
}
