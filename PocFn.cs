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
    public static class PocFn
    {
        [FunctionName(nameof(PocFn))]
        public static async Task<IActionResult> Run(
            [ServiceBusTrigger(Constants.QueueName, Connection = Constants.ServiceBusConnectionStringSetting)]Message eventMessage,
            ILogger logger
        )
        {
            var settings = ReminderSettings.GetReminderSettings();
            var request = JsonConvert.DeserializeObject<RequestModel>(Encoding.UTF8.GetString(eventMessage.Body));

            return new OkObjectResult($"Received {request.Message}");
        }
    }
}