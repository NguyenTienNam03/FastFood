using AppData.IService;
using AppData.Models;
using AppData.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
		public bool CreatPayment( string Payment , string mota , string? bankaccount , string? banknumber , string? bankname , string? Imageqr )
		{
            Payments payments = new Payments();

            payments.IDPayment = Guid.NewGuid();
			payments.Payment = Payment;
			payments.Bankaccount = bankaccount;
			payments.BankAccountNumber = banknumber;
			payments.Description = mota;
			payments.BankName = bankname;
			payments.ImageQR = Imageqr;
			payments.Status = 1;
			return _paymentsService.CreatePayment(payments);
    }

		// PUT api/<PaymentsController>/5
		[HttpPut("[action]")]
		public bool UpdatePayment(Guid id, string Payment, string mota, string? bankaccount, string? banknumber, string? bankname, string? Imageqr , int trangthai)
		{
            Payments payments = _paymentsService.GetAllPayments().FirstOrDefault(c => c.IDPayment == id);
            payments.Payment = Payment;
            payments.Bankaccount = bankaccount;
            payments.BankAccountNumber = banknumber;
            payments.Description = mota;
            payments.BankName = bankname;
            payments.ImageQR = Imageqr;
			payments.Status = trangthai;
            return _paymentsService.UpdatePayment(payments);
		}

		// DELETE api/<PaymentsController>/5
		[HttpPut("[action]")]
		public bool DeletePayment(Guid id)
		{
			return (_paymentsService.DeletePayment(id));
		}
	}
}
