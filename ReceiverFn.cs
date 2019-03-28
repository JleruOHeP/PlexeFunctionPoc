using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Plexe.Function.Models;
using Plexe.Function.Settings;

namespace Plexe.Function
{
    public static class ReceiverFn
    {
        [FunctionName(nameof(ReceiverFn))]
        public static async Task Run(
            [ServiceBusTrigger(Constants.QueueName, Connection = Constants.ServiceBusConnectionStringSetting)]Message eventMessage,
            ILogger logger
        )
        {
            var request = JsonConvert.DeserializeObject<RequestModel>(Encoding.UTF8.GetString(eventMessage.Body));            

            logger.LogInformation($"Received {request.Message}");
        }
    }
}