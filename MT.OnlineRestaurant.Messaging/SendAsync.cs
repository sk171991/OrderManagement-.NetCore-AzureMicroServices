using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace MT.OnlineRestaurant.Messaging
{
    public class SendAsync
    {
        const string ServiceBusConnectionString = "Endpoint=sb://capstoneapi.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=CLJQF4McnQ8UVBixOM+pTvK/Uo9ybJGESjQQMGnWscA=";
        const string TopicName = "itemoutofstock";
        static ITopicClient topicClient;
        public async Task SendMessagesAsync(OrderEntity orderEntity)
        {
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
            try
            {
                string messageBody = JsonConvert.SerializeObject(orderEntity);
                Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
                await topicClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }

    }
}
