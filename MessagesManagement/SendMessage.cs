using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;
using LoggingManagement;

namespace MessagesManagement
{
    public class SendMessage
    {
        private readonly ILogService _logService;
        const string ServiceBusConnectionString = "Endpoint=sb://capstoneapi.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=CLJQF4McnQ8UVBixOM+pTvK/Uo9ybJGESjQQMGnWscA=";
        const string TopicName = "itemoutofstock";
        static ITopicClient topicClient;
        public static async Task SendMessagesAsync(string msg)
        {
            
            try
            {
                topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
                //string messageBody = JsonConvert.SerializeObject(orderEntity);
                Message message = new Message(Encoding.UTF8.GetBytes(msg));
                await topicClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                //_logService.LogException(exception);
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
