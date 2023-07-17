using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModels
{
    public class CartDetailViewModels
    {
        public Guid IDCartDetail { get; set; }
        public Guid IDsetKey { get; set; }
        public Guid IdCart { get; set; }
        public Guid IdFood { get; set; }
        public string NameFood { get; set; }
        public string Image { get; set; }
        public int Quatity { get; set; }
        public decimal Price { get; set; }
    }
}
