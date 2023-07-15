using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class Setkey
    {
        [Key]
        public Guid IDSetKey { get; set; }
        [ForeignKey(nameof(MainDishes))]
        public Guid? IDMain { get; set; }
        [ForeignKey(nameof(ComboFastFood))]
        public Guid? IDCombo { get; set; }
        [ForeignKey(nameof(SideDishes))]
        public Guid? IDSide { get; set; }
        [ForeignKey(nameof(Drinks))]
        public Guid? IDDrink { get; set; }

        public virtual MainDishes MainDishes { get; set; }
        public virtual SideDishes SideDishes { get; set; }
        public virtual ComboFastFood ComboFastFood { get; set; }
        public virtual Drinks Drinks { get; set; }

        public IEnumerable<CartDetail> CartDetail { get; set; }
    }
}
