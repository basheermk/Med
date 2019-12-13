using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
    public class Payment : BaseReference
    {
        private tb_Payment Payments;
        public Payment() { }
        public Payment(tb_Payment crs) { Payments = crs; }
        public Payment(long paymentid) { Payments = _Entities.tb_Payment.FirstOrDefault(x => x.PaymentId == paymentid); }


        
        public long PaymentId { get { return Payments.PaymentId; } }
        public long UserId { get { return Payments.UserId; } }
        public bool PaidStatus { get { return Payments.PaidStatus; } }
        public decimal Amount { get { return Payments.Amount; } }
        public Nullable<long> PaymentType { get { return Payments.PaymentType; } }
        public System.DateTime TimeStamp { get { return Payments.TimeStamp; } }
        public System.Guid ParentGuid { get { return Payments.ParentGuid; } }
        public Nullable<int> PaymentMode { get { return Payments.PaymentMode; } }
        public bool IsActive { get { return Payments.IsActive; } }
        public long PackageID { get { return Payments.PackageID; } }
        public Nullable<System.DateTime> Expirydate { get { return Payments.Expirydate; } }
        public Nullable<decimal> REQUEST_ID { get { return Payments.REQUEST_ID; } }
        public Nullable<decimal> RESPONSE_ID { get { return Payments.RESPONSE_ID; } }

        public Payment ExampaymentDetails(long userId)
        {
            return _Entities.tb_Payment.Where(x => x.UserId == userId && x.PaymentType == 1).OrderByDescending(x => x.UserId).ToList().Select(y => new Payment(y)).FirstOrDefault();
        }
    }
}
