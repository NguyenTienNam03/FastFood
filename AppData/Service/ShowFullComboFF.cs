using AppData.IService;
using AppData.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
    public class ShowFullComboFF
    {
        public IComboFastFoodService _comboFastFoodService;
        private IDrinkService _drinkService;
        private IMainDishesService _mainDishesService;
        private ISideDishesService _sideDishesService;
        public ShowFullComboFF()
        {
            _comboFastFoodService = new ComboFastFoodService();
            _drinkService = new DrinkService();
            _mainDishesService = new MainDishesService();
            _sideDishesService = new SideDishesService();
        }
        public List<ComboFastFoodViewModel> ShowCombo()
        {
            var combo = from a in _comboFastFoodService.GetList()
                        join b in _drinkService.GetAllDrinks() on a.IDDrink equals b.IDDrink
                        join c in _mainDishesService.GetMainDishes() on a.IDMainDishes equals c.IDMainDishes
                        join d in _sideDishesService.GetAllSideDishes() on a.IDSideDishes equals d.IDSideDishes
                        select new ComboFastFoodViewModel
                        {
                            IDCombo = a.IDCombo,
                            SideDishes = d.NameSideDishes,
                            MainDishes = c.NameMainDishes,
                            Drink = b.NameDrink,
                            NameCombo = a.NameCombo,
                            Image = a.Image,
                            Price = a.Price,
                            PriceCombo = a.PriceCombo,
                            DescriptionCombo = a.DescriptionCombo,
                            Status = a.Status,

                        };
            return combo.ToList();
        }

        public ComboFastFoodViewModel GetByID(Guid id)
        {
            try
            {
                var combo = from a in _comboFastFoodService.GetList()
                            join b in _drinkService.GetAllDrinks() on a.IDDrink equals b.IDDrink
                            join c in _mainDishesService.GetMainDishes() on a.IDMainDishes equals c.IDMainDishes
                            join d in _sideDishesService.GetAllSideDishes() on a.IDSideDishes equals d.IDSideDishes
                            select new ComboFastFoodViewModel
                            {
                                IDCombo = a.IDCombo,
                                SideDishes = d.NameSideDishes,
                                MainDishes = c.NameMainDishes,
                                Drink = b.NameDrink,
                                NameCombo = a.NameCombo,
                                Image = a.Image,
                                Price = a.Price,
                                PriceCombo = a.PriceCombo,
                                DescriptionCombo = a.DescriptionCombo,
                                Status = a.Status,

                            };
                return combo.FirstOrDefault(c => c.IDCombo == id);
            }
            catch
            {
                return new ComboFastFoodViewModel();
            }
        }
    }
}
