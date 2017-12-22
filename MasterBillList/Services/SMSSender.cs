using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MasterBillList.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class SMSSender : ISMSSender
    {
        public SMSSender(IOptions<TwilioSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        public TwilioSenderOptions Options { get; } //set only via Secret Manager

        public Task SendSMSAsync(string recipient, string message)
        {
            return Execute(Options.TwilioAccountSID, Options.TwilioAccountAuthToken, Options.TwilioFromPhoneNumber, recipient, message);
        }

        public Task Execute(string accountSID, string authToken, string fromPhoneNumber, string recipient, string message)
        {
            TwilioClient.Init(accountSID, authToken);

            return MessageResource.CreateAsync(
                to: new PhoneNumber(recipient),
                from: new PhoneNumber(fromPhoneNumber),
                body: message);
        }
    }

}
