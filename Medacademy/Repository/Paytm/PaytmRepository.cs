using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medacademy.Repository.Paytm
{
    public class PaytmRepository
    {
        public static string MerchantID = "DjyhFp51887347334909"; //sibi patm

        public static string MerchantKey = "rLm7N%q4zmFNvuC5"; //sibi patm

        //public static string MerchantID = "xpodou27578647186144"; //onley for http://medacademy.co.in/

        //public static string MerchantKey = "R7%_QBMY_orzudAf"; //onley for http://medacademy.co.in/

        //public static string MerchantID = "gKKzzZ76264625452954"; // claint testID manu patm

        //public static string MerchantKey = "%Xmvca2gnT@zxFSI"; //claint testKey manu patm

        public static string OrderID = Guid.NewGuid().ToString().Replace("-","x");

        public static string Website = "WebSiteStaringTest";

        public static string CallBackUrl = "http://202.191.65.67/Paytm/paytmResponse";

    }
}