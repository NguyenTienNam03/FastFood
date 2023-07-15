using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BillController : ControllerBase
	{
		private IBillService _billService;
		private IBillDetailService _billDetailService;
		private ICartDetailService _cartDetailService;
		private ICustomerService _customerService;
		private IVoucherService _voucherService;
		private IPaymentService _paymentService;
		private IDrinkService _drinkService;
		private ISideDishesService _sideDishesService;
		private IMainDishesService _mainDishesService;
		private IComboFastFoodService _comboFastFoodService;

		public BillController()
		{
			_billDetailService = new BillDetailService();
			_billService = new BillService();
			_drinkService = new DrinkService();
			_mainDishesService = new MainDishesService();
			_sideDishesService = new SideDishesService();
			_comboFastFoodService = new ComboFastFoodService();
			_voucherService = new VoucherService();
			_paymentService = new PaymentService();
			_cartDetailService = new CartDetailService();	
			_customerService = new CustomerSevice();
		}

		// GET: api/<BillController>
		[HttpGet("[action]")]
		public IEnumerable<Bill> GetAllBill()
		{
			return _billService.GetBillList();
		}

		// GET api/<BillController>/5
		[HttpGet("[action]")]
		public Bill GetBillById(Guid id)
		{
			return _billService.GetByID(id);
		}

		// POST api/<BillController>
		[HttpPost("[action]")]
		public bool CreateBill(Bill bill)
		{
			return true;
		}

		// PUT api/<BillController>/5
		[HttpPut("[action]")]
		public void Put(int id, [FromBody] string value)
		{
		}
	}
}
