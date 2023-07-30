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
        public IEnumerable<Bill> GetAllBill(Guid id)
        {
            return _billService.GetBillList().OrderByDescending(c => c.CreateBill).Where(c => c.IDCustomer == id).ToList();
        }

        // GET api/<BillController>/5
        [HttpGet("[action]")]
        public Bill GetBillById(Guid id)
        {
            return _billService.GetByID(id);
        }
        [HttpGet("[action]")]
        public List<BillDetail> Getthelatestvalue(Guid idcustomer)
        {
           var idbill =  _billService.GetBillList().OrderByDescending(c => c.CreateBill).First(c =>  c.IDCustomer == idcustomer).IDBill;
            return _billDetailService.GetBillDetailList(idbill);
        }
        [HttpGet("[action]")]
        public List<BillDetail> GetBillDetailByIdbill(Guid id)
        {
            var idbill = _billService.GetBillList().FirstOrDefault(c => c.IDBill == id).IDBill;
            return _billDetailService.GetBillDetailList(idbill);
        }
        // POST api/<BillController>
        [HttpPost("[action]")]
        public bool CreateBill(Guid idvoucher, Guid idcustom, Guid idpay,  string? note)
        {
            try
            {
                var user = _customerService.GetAllCus().FirstOrDefault(c => c.IDCustomer == idcustom);
                Bill bill = new Bill();
                bill.IDBill = Guid.NewGuid();
                bill.IDVoucher = _voucherService.GetAllVouchers().First(c => c.IDVoucher == idvoucher).IDVoucher;
                bill.IDCustomer = _customerService.GetAllCus().FirstOrDefault(c => c.IDCustomer == idcustom).IDCustomer;
                bill.IDPayment = _paymentService.GetAllPayments().FirstOrDefault(c => c.IDPayment == idpay).IDPayment;
                bill.InvoiceCode = Convert.ToString(bill.IDBill).Substring(0, 8).ToUpper();
                bill.Quatity = 0;
                bill.NameReceiver = user.NameCustomer;
                bill.PhoneReceiver = user.PhoneNumber;
                bill.CityReceiver = user.City;
                bill.DistrictReceiver = user.District;
                bill.TotalAmount = 0;
                bill.TransportFee = 1;
                bill.TotalPayment = 0;
                bill.CreateBill = DateTime.Now;
                bill.OrderDate = DateTime.Now;
                bill.DeliveryDate = DateTime.Now;
                bill.DateOfPayment = DateTime.Now;
                bill.Note = note;
                bill.Status = 1;

                _billService.CreateBill(bill);

                foreach (var cartdetail in _cartDetailService.GetAllCartDetail(idcustom))
                {
                    BillDetail billDetail = new BillDetail()
                    {
                        IDBill = bill.IDBill,
                        IDBillDetail = Guid.NewGuid(),
                        Price = cartdetail.Price,
                        Quatity = cartdetail.Quatity,
                        IDFood = cartdetail.IDFood,
                        Image = cartdetail.Image,
                        NameFood = cartdetail.NameFood,
                    };
                    _billDetailService.CreateBillDetail(billDetail);
                    _cartDetailService.DeleteCartDetail(cartdetail.IDCartDetail);
                }
                var billdetail = _billDetailService.GetBillDetailList(bill.IDBill);
                Bill Updatebill = _billService.GetBillList().FirstOrDefault(C => C.IDBill == bill.IDBill);
                Updatebill.IDVoucher = bill.IDVoucher;
                Updatebill.IDCustomer = bill.IDCustomer;
                Updatebill.IDPayment = bill.IDPayment;
                Updatebill.InvoiceCode = bill.InvoiceCode;
                Updatebill.Quatity = billdetail.Sum(c => c.Quatity);
                Updatebill.NameReceiver = bill.NameReceiver;
                Updatebill.PhoneReceiver = bill.PhoneReceiver;
                Updatebill.CityReceiver = bill.CityReceiver;
                Updatebill.DistrictReceiver = bill.DistrictReceiver;
                Updatebill.TotalAmount = billdetail.Sum(c => c.Quatity * c.Price);
                Updatebill.TransportFee = bill.TransportFee;
                Updatebill.TotalPayment = billdetail.Sum(c => c.Quatity * c.Price) + bill.TransportFee;
                Updatebill.CreateBill = bill.CreateBill;
                Updatebill.OrderDate = bill.OrderDate;
                Updatebill.DeliveryDate = bill.DeliveryDate;
                Updatebill.DateOfPayment = bill.DateOfPayment;
                Updatebill.Note = note;
                Updatebill.Status = 1;
                _billService.UpdateBill(Updatebill);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("[action]")]
        public bool UpdateBill(Guid idbill, Guid idvoucher, Guid idpay, string name , string phone , string city , string distric, string? note)
        {
            var voucher = _voucherService.GetAllVouchers().First(c => c.IDVoucher == idvoucher);
            Bill Updatebill = _billService.GetBillList().FirstOrDefault(C => C.IDBill == idbill);
            Updatebill.IDVoucher = Updatebill.IDVoucher;
            Updatebill.IDVoucher = voucher.IDVoucher;
            Updatebill.IDCustomer = Updatebill.IDCustomer;
            Updatebill.IDPayment = _paymentService.GetAllPayments().First(c => c.IDPayment == idpay).IDPayment;
            Updatebill.InvoiceCode = Updatebill.InvoiceCode;
            Updatebill.Quatity = Updatebill.Quatity;
            Updatebill.NameReceiver = name;
            Updatebill.PhoneReceiver = phone;
            Updatebill.CityReceiver = city;
            Updatebill.DistrictReceiver = distric;
            Updatebill.TotalAmount = Updatebill.TotalAmount;
            Updatebill.TransportFee = Updatebill.TransportFee;
            Updatebill.TotalPayment = Updatebill.TotalPayment - (Updatebill.TotalPayment * voucher.VoucherValue / 100);
            Updatebill.OrderDate = DateTime.Now;
            Updatebill.DeliveryDate = DateTime.Now;
            Updatebill.DateOfPayment = DateTime.Now;
            Updatebill.Note = note;
            Updatebill.Status = 1;
            _billService.UpdateBill(Updatebill);
            return true;
        }
        // PUT api/<BillController>/5
        [HttpPut("[action]")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
