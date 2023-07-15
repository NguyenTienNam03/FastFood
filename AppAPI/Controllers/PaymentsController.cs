using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentsController : ControllerBase
	{
		private IPaymentService _paymentsService;
		public PaymentsController()
		{
			_paymentsService = new PaymentService();
		}
		// GET: api/<PaymentsController>
		[HttpGet("[action]")]
		public IEnumerable<Payments> GetAllPayment()
		{
			return _paymentsService.GetAllPayments();
		}

		// GET api/<PaymentsController>/5
		[HttpGet("[action]")]
		public Payments GetByID(Guid id)
		{
			return _paymentsService.GetPaymentByID(id);
		}

		// POST api/<PaymentsController>
		[HttpPost("[action]")]
		public bool CreatPayment(Payments payments)
		{
			payments.IDPayment = Guid.NewGuid();
			return _paymentsService.CreatePayment(payments);
		}

		// PUT api/<PaymentsController>/5
		[HttpPut("[action]")]
		public bool UpdatePayment(Payments payments)
		{
			return _paymentsService.UpdatePayment(payments);
		}

		// DELETE api/<PaymentsController>/5
		[HttpDelete("[action]")]
		public bool DeletePayment(Guid id)
		{
			return (_paymentsService.DeletePayment(id));
		}
	}
}
