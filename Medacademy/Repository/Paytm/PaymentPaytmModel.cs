using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medacademy.Repository.Paytm
{
    public class PaymentPaytmModel
    {
        public long PaymentId { get; set; }
        public long UserId { get; set; }
        public bool PaidStatus { get; set; }
        public decimal Amount { get; set; }
        public Nullable<long> PaymentType { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public System.Guid ParentGuid { get; set; }
        public Nullable<int> PaymentMode { get; set; }
        public bool IsActive { get; set; }
        public long PackageID { get; set; }
        public Nullable<System.DateTime> Expirydate { get; set; }
        public Nullable<decimal> REQUEST_ID { get; set; }
        public Nullable<decimal> RESPONSE_ID { get; set; }
        public string PakageName { get; set; }
    }
}