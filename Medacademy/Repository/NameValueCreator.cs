using Medacademy.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Medacademy.Repository
{
    public class NameValueCreator
    {
        public static SortedDictionary<string, string> SortNameValueCollection(NameValueCollection nvc)
        {
            SortedDictionary<string, string> sortedDict = new SortedDictionary<string, string>();
            foreach (String key in nvc.AllKeys)
                sortedDict.Add(key, nvc[key]);
            return sortedDict;
        }
        public static SortedDictionary<string, string> SortNameValueCollection(UserModel model)
        {
            var datas = model.LI_Pacages.Where(x => x.PackageID == model.TempID1).FirstOrDefault();
           
            string amount = Convert.ToString(Convert.ToInt64((Convert.ToInt64(datas.Amount) - Convert.ToInt64(datas.DiscountAmount))));
            SortedDictionary<string, string> sortedDict = new SortedDictionary<string, string>();
            sortedDict.Add("account_id", "21159");
            sortedDict.Add("address", model.Address);
            sortedDict.Add("amount", amount);
            sortedDict.Add("channel", "10");
            sortedDict.Add("city", "null");
            sortedDict.Add("country", "IND");
            sortedDict.Add("currency", "INR");
            sortedDict.Add("description", "null");
            sortedDict.Add("email", model.Email);
            sortedDict.Add("mode", "LIVE");
            sortedDict.Add("name", model.FirstName);
            sortedDict.Add("phone", model.ContactNo);
            sortedDict.Add("postal_code", model.Pin);
            sortedDict.Add("reference_no", "691F4476AB");
            sortedDict.Add("return_url", model.ReturnUrl);
            sortedDict.Add("state", model.State);
            
            return sortedDict;
        }
    }
}