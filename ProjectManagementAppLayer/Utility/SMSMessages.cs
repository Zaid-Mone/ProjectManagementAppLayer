using Infobip.Api.Client;
using Infobip.Api.Client.Api;
using Infobip.Api.Client.Model;
using Microsoft.Extensions.Configuration;
using ProjectManagementBusinessLayer.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.Utility
{
    public class SMSMessages 
    {
        public static void SendSMSMessage(string MESSAGE_TEXT, string key)
        {
            var mySetting = key;
            string BASE_URL = "https://5vzxyz.api.infobip.com";
            string API_KEY = mySetting;
            string SENDER = "InfoSMS";
            string RECIPIENT = "962776934286";
            var configuration = new Configuration()
            {
                BasePath = BASE_URL,
                ApiKeyPrefix = "App",
                ApiKey = API_KEY
            };

            var sendSmsApi = new SendSmsApi(configuration);

            var smsMessage = new SmsTextualMessage()
            {
                From = SENDER,
                Destinations = new List<SmsDestination>()
                {
                    new SmsDestination(to: RECIPIENT)
                },
                Text = MESSAGE_TEXT
            };

            var smsRequest = new SmsAdvancedTextualRequest()
            {
                Messages = new List<SmsTextualMessage>() { smsMessage }
            };

            try
            {
                var smsResponse = sendSmsApi.SendSmsMessage(smsRequest);

                Console.WriteLine("Response: " + smsResponse.Messages.FirstOrDefault());
            }
            catch (ApiException apiException)
            {
                Console.WriteLine("Error occurred! \n\tMessage: {0}\n\tError content", apiException.ErrorContent);
            }
        }
    }
}
