using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterBillList.Services
{
    public interface IPushbulletSender
    {
        Task SendEphemeralAsync(string apiKey, string encryptionKey, string message, string encryptionPassword = null);
    }
}
