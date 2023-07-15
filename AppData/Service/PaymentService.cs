using AppData.IService;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
	public class PaymentService : IPaymentService
	{
		private DB_Context _context;
		public PaymentService()
		{
			_context = new DB_Context();
		}
		public bool CreatePayment(Payments payments)
		{
			try
			{
				_context.payments.Add(payments);
				_context.SaveChanges();
				return true;
			}catch (Exception ex)
			{
				return false;
			}
		}

		public bool DeletePayment(Guid id)
		{
			try
			{
				var payment = _context.payments.FirstOrDefault(c => c.IDPayment == id);
				if (payment != null)
				{
					payment.Status = 0;
				}
				_context.payments.Update(payment);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public List<Payments> GetAllPayments()
		{
			return _context.payments.ToList();
		}

		public Payments GetPaymentByID(Guid id)
		{
			return _context.payments.FirstOrDefault(c => c.IDPayment == id);
		}

		public bool UpdatePayment(Payments payments)
		{
			try
			{
				var payment = _context.payments.FirstOrDefault(c => c.IDPayment == payments.IDPayment);
				if (payment != null)
				{
					payment.BankName = payments.BankName;
					payment.BankAccountNumber = payments.BankAccountNumber;
					payment.Bankaccount = payments.Bankaccount;
					payment.ImageQR = payments.ImageQR;
					payment.Status = payments.Status;
				}
				_context.payments.Update(payment);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
