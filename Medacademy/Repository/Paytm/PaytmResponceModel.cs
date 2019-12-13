using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medacademy.Repository.Paytm
{
    public class PaytmResponceModel
    {
        public decimal RESPONSE_ID { get; set; }
        public decimal REQUEST_ID { get; set; }
        public long USER_ID { get; set; }
        public long PAKAGE_ID { get; set; }
        public string MID { get; set; }
        public string TXNID { get; set; }
        public string ORDERID { get; set; }
        public string BANKTXNID { get; set; }
        public string TXNAMOUNT { get; set; }
        public string CURRENCY { get; set; }
        public string STATUS { get; set; }
        public string RESPCODE { get; set; }
        public string RESPMSG { get; set; }
        public string TXNDATE { get; set; }
        public string GATEWAYNAME { get; set; }
        public string BANKNAME { get; set; }
        public string PAYMENTMODE { get; set; }
        public string CHECKSUMHASH { get; set; }
        public string BIN_NUMBER { get; set; }
        public string CARD_LAST_NUMS { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime TimeStamp { get; set; }
    }
}