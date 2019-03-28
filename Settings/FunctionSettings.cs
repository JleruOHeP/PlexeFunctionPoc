using System;

namespace Plexe.Function.Settings
{
    public class FunctionSettings
    {
        public string ServiceBusConnectionString { get; set; }

        public string ServiceBusQueueName => Constants.QueueName;

        public static FunctionSettings GetFunctionSettings() =>
            new FunctionSettings
            {
                ServiceBusConnectionString = Environment.GetEnvironmentVariable(Constants.ServiceBusConnectionStringSetting)
            };
    }
}
