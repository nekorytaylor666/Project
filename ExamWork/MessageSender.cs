using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ExamWork
{
    public class MessageSender : ISmsSender
    {
        const string _accountSid = "ACfa1dc641f65b4553982e12d1ed28e783";
        const string _authToken = "de34e33c7f9562be46da7dbe240ec7dc";
        const string _numFrom = "+19596666446"; // номер данный твилио по умолчанию, больше никакой не работает

        public void SendMessage(string numTo, string content)
        {  
                TwilioClient.Init(_accountSid, _authToken);

                var message = MessageResource.Create(
                    from: new Twilio.Types.PhoneNumber(_numFrom),
                    body: content,
                    to: new Twilio.Types.PhoneNumber(numTo)
                );

                Console.WriteLine(message.Sid);
        }
    }
}
