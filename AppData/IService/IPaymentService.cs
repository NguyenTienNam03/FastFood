using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
	public interface IPaymentService
	{
		public bool CreatePayment(Payments payments);
		public bool UpdatePayment(Payments payments);
		public bool DeletePayment(Guid  id);
		public List<Payments> GetAllPayments();
		public Payments GetPaymentByID(Guid id);
	}
}
