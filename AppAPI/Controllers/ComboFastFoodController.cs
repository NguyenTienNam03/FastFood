using AppData.IService;
using AppData.Models;
using AppData.Service;
using AppData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Security.Cryptography.Xml;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ComboFastFoodController : ControllerBase
	{
		private IComboFastFoodService _comboFastFoodService;
		private ShowFullComboFF _comboviewmodel;
		private IDrinkService _drinkService;
		private ISideDishesService _dishesService;
		private IMainDishesService _mainDishesService;
		public ComboFastFoodController()
		{
			_comboviewmodel = new ShowFullComboFF();
			_comboFastFoodService = new ComboFastFoodService();
			_drinkService = new DrinkService();
			_dishesService = new SideDishesService();
			_mainDishesService = new MainDishesService();
		}
		// GET: api/<ComboFastFoodController>
		[HttpGet("[action]")]
		public IEnumerable<ComboFastFoodViewModel> ShowComboFF()
		{
			return _comboviewmodel.ShowCombo();
		}

		// GET api/<ComboFastFoodController>/5
		[HttpGet("[action]")]
		public ComboFastFoodViewModel ShowComboFFByID(Guid id)
		{
			return _comboviewmodel.GetByID(id);
		}

		[HttpGet("[action]")]
		public IEnumerable<ComboFastFood> GetAllComBo()
		{
			return _comboFastFoodService.GetList();
		}
		[HttpGet("[action]")]
		public ComboFastFood GetComboByID(Guid id)
		{
			return _comboFastFoodService.GetComboFastFoodByID(id);
		}

		// POST api/<ComboFastFoodController>
		// Combo FastFood
		[HttpPost("[action]")]
		public bool AddCombo(Guid? idside, Guid? idmain, Guid? iddrink, string name, string anh, string mota, int trangthai)
		{
			var drink = _drinkService.GetAllDrinks().FirstOrDefault(c => c.IDDrink == iddrink);
			var side = _dishesService.GetAllSideDishes().FirstOrDefault(c => c.IDSideDishes == idside);
			var main = _mainDishesService.GetMainDishes().FirstOrDefault(c => c.IDMainDishes == idmain);
			if (iddrink == null && idmain == null && idside == null || _comboFastFoodService.GetList().Any(c => c.IDMainDishes == iddrink && c.IDDrink == iddrink && c.IDSideDishes == idside))
			{
				return false;
			}
			else
			{
				if (iddrink == null && idmain == null && idside != null || _comboFastFoodService.GetList().Any(c => c.IDMainDishes == null && c.IDDrink == iddrink && c.IDSideDishes == idside))
				{
					return false;
				}
				else if (iddrink == null && idmain != null && idside == null || _comboFastFoodService.GetList().Any(c => c.IDMainDishes == iddrink && c.IDDrink == null && c.IDSideDishes == idside))
				{
					return false;
				}
				else if (iddrink != null && idmain == null && idside == null || _comboFastFoodService.GetList().Any(c => c.IDMainDishes == iddrink && c.IDDrink == iddrink && c.IDSideDishes == null))
				{
					return false;
				}
				else
				{
					ComboFastFood comboFastFood = new ComboFastFood();
					comboFastFood.IDCombo = Guid.NewGuid();

					comboFastFood.IDSideDishes = (idside == null ? null : side.IDSideDishes);
					comboFastFood.IDMainDishes = (idmain == null ? null : main.IDMainDishes);
					comboFastFood.IDDrink = (iddrink == null ? null : drink.IDDrink);
					comboFastFood.NameCombo = name;
					comboFastFood.Image = anh;
					comboFastFood.Price = (iddrink == null ? 0 : drink.Price) + (idside == null ? 0 : side.Price) + (idmain == null ? 0 : main.Price);
					comboFastFood.PriceCombo = (iddrink == null ? 0 : drink.Price) + (idside == null ? 0 : side.Price) + (idmain == null ? 0 : main.Price) - ((iddrink == null ? 0 : drink.Price) + (idside == null ? 0 : side.Price) + (idmain == null ? 0 : main.Price) * 10 / 100);
					comboFastFood.DescriptionCombo = mota;
					comboFastFood.Status = trangthai;
					return _comboFastFoodService.CreateCombo(comboFastFood);
				}
			}
		}
		// PUT api/<ComboFastFoodController>/5
		[HttpPut("[action]")]
		public bool UpdateCombo(Guid id, Guid? idside, Guid? idmain, Guid? iddrink, string name, string anh, decimal gia, decimal giacombo, string mota, int trangthai)
		{
			ComboFastFood comboFastFood = _comboFastFoodService.GetList().FirstOrDefault(c => c.IDCombo == id);
			comboFastFood.IDSideDishes = _dishesService.GetAllSideDishes().FirstOrDefault(c => c.IDSideDishes == idside).IDSideDishes;
			comboFastFood.IDMainDishes = _mainDishesService.GetMainDishes().FirstOrDefault(c => c.IDMainDishes == idmain).IDMainDishes;
			comboFastFood.IDDrink = _drinkService.GetAllDrinks().FirstOrDefault(c => c.IDDrink == iddrink).IDDrink;
			comboFastFood.NameCombo = name;
			comboFastFood.Image = anh;
			comboFastFood.Price = gia;
			comboFastFood.PriceCombo = giacombo;
			comboFastFood.DescriptionCombo = mota;
			comboFastFood.Status = trangthai;
			return _comboFastFoodService.UpdateCombo(comboFastFood);
		}

		// DELETE api/<ComboFastFoodController>/5
		[HttpPut("[action]")]
		public bool DeleteCombo(Guid id)
		{
			return _comboFastFoodService.DeleteCombo(id);
		}
		// Drink
		[HttpGet("[action]")]
		public List<Drinks> GetAllDrink()
		{
			return _drinkService.GetAllDrinks();
		}
		[HttpGet("[action]")]
		public Drinks GetByID(Guid id)
		{
			return _drinkService.GetDrinkById(id);
		}
		[HttpPost("[action]")]
		public bool CreateDrink(string name, string image, decimal gia, decimal khoiluong)
		{
			if (_drinkService.GetAllDrinks().Any(c => c.NameDrink == name && c.Price == gia && c.Mass == khoiluong) == false)
			{
				Drinks drinks = new Drinks();
				drinks.IDDrink = Guid.NewGuid();
				drinks.NameDrink = name;
				drinks.Price = gia;
				drinks.Image = image;
				drinks.Mass = khoiluong;
				drinks.Status = 1;
				return _drinkService.CreateDrink(drinks);
			}
			else
			{
				return false;
			}

		}
		[HttpPut("[action]")]
		public bool UpdateDrink(Guid id, string name, string image, decimal gia, decimal khoiluong, int trangthai)
		{

			Drinks drinks = _drinkService.GetAllDrinks().FirstOrDefault(c => c.IDDrink == id);
			drinks.NameDrink = name;
			drinks.Price = gia;
			drinks.Image = image;
			drinks.Mass = khoiluong;
			drinks.Status = trangthai;
			return _drinkService.UpdateDrink(drinks);

		}
		[HttpPut("[action]")]
		public bool DeleteDrink(Guid id)
		{
			return _drinkService.DeleteDrink(id);
		}

		// Sidedishes
		[HttpGet("[action]")]
		public List<SideDishes> GetAllSidedishes()
		{
			return _dishesService.GetAllSideDishes();
		}
		[HttpGet("[action]")]
		public SideDishes GetSideDishesByid(Guid id)
		{
			return _dishesService.GetSideDishesByID(id);
		}
		[HttpPost("[action]")]
		public bool CreateSideDishes(string name, string image, string mota, decimal gia, decimal khoiluong)
		{
			if (_dishesService.GetAllSideDishes().Any(c => c.NameSideDishes.Contains(name) && c.Mass == khoiluong && c.DescriptionSideDishes == mota && c.Price == gia) == false)
			{
				SideDishes sideDishes = new SideDishes();
				sideDishes.IDSideDishes = Guid.NewGuid();
				sideDishes.NameSideDishes = name;
				sideDishes.Image = image;
				sideDishes.Price = gia;
				sideDishes.DescriptionSideDishes = mota;
				sideDishes.Mass = khoiluong;
				sideDishes.Status = 1;
				return _dishesService.CreateSideDishes(sideDishes);
			}
			else
			{
				return false;
			}

		}
		[HttpPut("[action]")]
		public bool UpdateDishes(Guid id, string name, string image, string mota, decimal gia, decimal khoiluong, int trangthai)
		{

			SideDishes sideDishes = _dishesService.GetAllSideDishes().FirstOrDefault(c => c.IDSideDishes == id);
			sideDishes.NameSideDishes = name;
			sideDishes.Image = image;
			sideDishes.Price = gia;
			sideDishes.Mass = khoiluong;
			sideDishes.Status = trangthai;
			return _dishesService.UpdateSideDishes(sideDishes);

		}

		[HttpPut("[action]")]
		public bool DeleteSideDishes(Guid id)
		{
			return _dishesService.DeleteSideDishes(id);
		}

		//MainDishes
		[HttpGet("[action]")]
		public List<MainDishes> GetAllMainDihes()
		{
			return _mainDishesService.GetMainDishes();
		}
		[HttpGet("[action]")]
		public MainDishes GetMainDishesById(Guid id)
		{
			return _mainDishesService.GetMainDishesByID(id);
		}

		[HttpPost("[action]")]
		public bool CreateMainDishes(string name, string anh, decimal gia, decimal khoiluong, string mota)
		{
			if (_mainDishesService.GetMainDishes().Any(c => c.NameMainDishes.Contains(name) && c.Mass == khoiluong && c.DescriptionMainDishes == mota) == false)
			{
				MainDishes mainDishes = new MainDishes();
				mainDishes.IDMainDishes = Guid.NewGuid();
				mainDishes.NameMainDishes = name;
				mainDishes.Image = anh;
				mainDishes.Price = gia;
				mainDishes.Mass = khoiluong;
				mainDishes.DescriptionMainDishes = mota;
				mainDishes.Status = 1;
				return _mainDishesService.CreateMainDishes(mainDishes);
			}
			else
			{
				return false;
			}

		}
		[HttpPut("[action]")]
		public bool UpdateMainDishes(Guid id, string name, string anh, decimal gia, decimal khoiluong, string mota, int trangthai)
		{

			MainDishes mainDishes = _mainDishesService.GetMainDishes().FirstOrDefault(c => c.IDMainDishes == id);
			mainDishes.NameMainDishes = name;
			mainDishes.Image = anh;
			mainDishes.Price = gia;
			mainDishes.Mass = khoiluong;
			mainDishes.DescriptionMainDishes = mota;
			mainDishes.Status = trangthai;
			return _mainDishesService.UpdateMainDishes(mainDishes);


		}
		[HttpPut("[action]")]
		public bool DeleteMainDishes(Guid id)
		{
			return _mainDishesService.DeleteMainDishes(id);
		}
	}
}
