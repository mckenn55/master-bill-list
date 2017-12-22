using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterBillList.Services
{
    public class TwilioSenderOptions
    {
        public string TwilioAccountSID { get; set; }
        public string TwilioAccountAuthToken { get; set; }
        public string TwilioFromPhoneNumber { get; set; }
    }
}
