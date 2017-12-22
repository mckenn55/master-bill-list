using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterBillList.Services
{
    public interface ISMSSender
    {
        Task SendSMSAsync(string recipient, string message);
    }
}
