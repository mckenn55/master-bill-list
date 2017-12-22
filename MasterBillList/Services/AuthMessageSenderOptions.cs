using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterBillList.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string SendEmailFromAddress { get; set; }
        public string SendEmailFromName { get; set; }
    }
}
