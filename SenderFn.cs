using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Plexe.Function.Models;
using Plexe.Function.Settings;

namespace Plexe.Function
{
    public static class SenderFn
    {
        [FunctionName(nameof(SenderFn))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req,
            ILogger logger
        )
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var message = new Message(Encoding.UTF8.GetBytes(new RequestModel
            {
                Message = requestBody
            }));

            var settings = FunctionSettings.GetFunctionSettings();
            var queueClient = new QueueClient(settings.ServiceBusConnectionString, settings.ServiceBusQueueName);

            await queueClient.SendAsync(message);
            await queueClient.CloseAsync();

            return new OkObjectResult($"Request sent - {requestBody}");
        }
    }
}