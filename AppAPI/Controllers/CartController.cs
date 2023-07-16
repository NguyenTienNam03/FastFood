using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private ICartService _cartService;
		private ICartDetailService _cartDetailService;
		private ISetKeyService _setkeyservice;
		private IComboFastFoodService _comboFastFoodService;
		private ISideDishesService _sideDishesService;
		private IDrinkService _drinkService;
		private IMainDishesService _mainDishesService;
		private ICustomerService _customerService;
		public CartController()
		{
			_cartDetailService = new CartDetailService();
			_comboFastFoodService = new ComboFastFoodService();
			_sideDishesService = new SideDishesService();
            _setkeyservice = new SetKeyService();
            _drinkService = new DrinkService();
			_mainDishesService = new MainDishesService();
			_cartService = new CartService();
			_customerService = new CustomerSevice();
		}

		[HttpGet("[action]")]
		public List<Cart> GetAllCart()
		{
			return _cartService.GetAllCart();
		}
		// POST api/<CartController>
		[HttpPost("[action]")]
		public bool CreateCart(Guid id)
		{
			if (_cartService.GetAllCart().Any(c => c.IDCart == id) == false)
			{
				Cart cart = new Cart();
				cart.IDCart = _customerService.GetAllCus().First(C => C.IDCustomer == id).IDCustomer;
				cart.Description = "Gio hang cua " + Convert.ToString(_customerService.GetAllCus().First(C => C.IDCustomer == cart.IDCart).NameCustomer);
				return _cartService.CreateCart(cart);
			}
			else
			{
				return false;
			}

		}

		[HttpPost("[action]")]
		public bool AddToCart(Guid idfood , Guid idcus)
		{
            //var combo = _comboFastFoodService.GetList().FirstOrDefault(c => c.IDCombo == idfood);
            //var drink = _drinkService.GetAllDrinks().FirstOrDefault(c => c.IDDrink == idfood);
            //var main = _mainDishesService.GetMainDishes().FirstOrDefault(c => c.IDMainDishes == idfood);
            //var side = _sideDishesService.GetAllSideDishes().FirstOrDefault(c => c.IDSideDishes == idfood);
            if (_cartService.GetAllCart().Any(c => c.IDCart == idcus))
			{
                if (_cartDetailService.GetAllCartDetail(idcus).Any(c => c.IDFood == idfood))
                {
                    CartDetail cartDetail = _cartDetailService.GetAllCartDetail(idcus).FirstOrDefault(c => c.IDFood == idfood);
                    cartDetail.Quatity = cartDetail.Quatity + 1;
                    return _cartDetailService.UpdateCartDetail(cartDetail);
                }
                else
                {
					// Tao bang chung
					Setkey setkey = new Setkey();
					setkey.IDSetKey = Guid.NewGuid();
					if(_mainDishesService.GetMainDishes().Any(c => c.IDMainDishes == idfood) == true)
					{
                        setkey.IDMain = _mainDishesService.GetMainDishes().First(c => c.IDMainDishes == idfood).IDMainDishes == idfood ? idfood : null;
                    } else if ( _comboFastFoodService.GetList().Any(c => c.IDCombo == idfood) == true)
					{
                        setkey.IDCombo = _comboFastFoodService.GetList().FirstOrDefault(c => c.IDCombo == idfood).IDCombo == idfood ? idfood : null;
                    } else if ( _drinkService.GetAllDrinks().Any(c => c.IDDrink == idfood) == true)
					{
                        setkey.IDDrink = _drinkService.GetAllDrinks().FirstOrDefault(c => c.IDDrink == idfood).IDDrink == idfood ? idfood : null;
                    } else
					{
                        setkey.IDSide = _sideDishesService.GetAllSideDishes().FirstOrDefault(c => c.IDSideDishes == idfood).IDSideDishes == idfood ? idfood : null;
                    }
                    _setkeyservice.CreateSetKey(setkey);
                    // them vao gioi hang chi tiet
                    CartDetail cartDetail = new CartDetail();
                    cartDetail.IDCartDetail = Guid.NewGuid();
					cartDetail.IDsetkey = _setkeyservice.GetSetKeys().FirstOrDefault(c => c.IDSetKey == setkey.IDSetKey).IDSetKey;
                    cartDetail.IDCart = _cartService.GetAllCart().FirstOrDefault(c => c.IDCart == idcus).IDCart;
                    if (_setkeyservice.GetSetKeys().FirstOrDefault(c => c.IDSetKey == setkey.IDSetKey).IDCombo != null)
                    {
                        cartDetail.IDFood = idfood;
                        cartDetail.Price = _comboFastFoodService.GetList().FirstOrDefault(c => c.IDCombo == idfood).Price;
                    }
                    else if (_setkeyservice.GetSetKeys().FirstOrDefault(c => c.IDSetKey == setkey.IDSetKey).IDDrink != null)
                    {
                        cartDetail.IDFood = idfood;
                        cartDetail.Price = _drinkService.GetAllDrinks().FirstOrDefault(c => c.IDDrink == idfood).Price;
                    }
                    else if (_setkeyservice.GetSetKeys().FirstOrDefault(c => c.IDSetKey == setkey.IDSetKey).IDMain != null)
                    {
                        cartDetail.IDFood = idfood;
                        cartDetail.Price = _mainDishesService.GetMainDishes().FirstOrDefault(c => c.IDMainDishes == idfood).Price;
                    }
					else
                    {
                        cartDetail.IDFood = idfood;
                        cartDetail.Price = _sideDishesService.GetAllSideDishes().FirstOrDefault(c => c.IDSideDishes == idfood).Price;
                    }
                    cartDetail.Quatity = 1;
                    return _cartDetailService.CreateCartDetail(cartDetail); // them vao 

					// them xong xoa bang trung gian
					//return _setkeyservice.DeleteSetKey(Guid.Parse("965a103c-07f0-4004-afd7-7798f7e6aef7"));
                }
            } else
			{
				CreateCart(idcus);
				return AddToCart(idfood, idcus);

            }
			
		}


		// show all item trong gio hang của khách hàng
		[HttpGet("[action]")]
		public List<CartDetail> ShowCartDetail(Guid id) // Truyen id cart hoac id customer
		{
			return _cartDetailService.GetAllCartDetail(id);
		}
		// xoa item ra khoi gio hang

		[HttpDelete("[action]")]
		public bool DeleteCartDetail(Guid id)
		{
			return _cartDetailService.DeleteCartDetail(id);
		}
	}
}
