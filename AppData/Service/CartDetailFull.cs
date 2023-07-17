using AppData.IService;
using AppData.Models;
using AppData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
    public class CartDetailFull
    {
        private IDrinkService _drinkService;
        private IComboFastFoodService _comboFastFoodService;
        private ISideDishesService _sdishesService;
        private IMainDishesService _mainDishesService;
        private ISetKeyService setKeyService;
        private ICartDetailService _cartDetailService;
        private DB_Context _Context;
        public CartDetailFull()
        {
            _Context = new DB_Context();
            _cartDetailService = new CartDetailService();
            _comboFastFoodService = new ComboFastFoodService();
            _drinkService = new DrinkService();
            _mainDishesService = new MainDishesService();
            _sdishesService = new SideDishesService();
        }

        public List<CartDetailViewModels> GetAllFullCartDetail()
        {
            var cartdetail = from f in _Context.cartDetails.ToList()
                             join a in setKeyService.GetSetKeys() on f.IDsetkey equals a.IDSetKey
                             join b in _drinkService.GetAllDrinks() on a.IDDrink equals b.IDDrink
                             join c in _sdishesService.GetAllSideDishes() on a.IDSide equals c.IDSideDishes
                             join d in _comboFastFoodService.GetList() on a.IDCombo equals d.IDCombo
                             join e in _mainDishesService.GetMainDishes() on a.IDMain equals e.IDMainDishes
                             select new CartDetailViewModels
                             {
                                IDCartDetail = f.IDCartDetail,
                                IdCart = f.IDCart,
                                IdFood = f.IDFood,
                                IDsetKey = a.IDSetKey,

                             };
            return cartdetail.ToList();

        }
    }
}
