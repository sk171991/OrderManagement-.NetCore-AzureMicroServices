using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.OrderAPI.Controllers;
using MT.OnlineRestaurant.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.OrderAPI
{
    public interface IPriceChangeServiceBusConsumer
    {
        void RegisterOnMessageHandlerAndReceiveMessages();
        Task CloseQueueAsync();
    }
    public class PriceChangeServiceBusConsumer : IPriceChangeServiceBusConsumer
    {
        private static ISubscriptionClient subscriptionClient;
        private static ITopicClient topicClient;

        public PriceChangeServiceBusConsumer()
        {
            string ServiceBusConnectionString = "Endpoint=sb://pricechange.servicebus.windows.net/;SharedAccessKeyName=updatepricecart;SharedAccessKey=40GSzf12A9FWauO/3K63+VVLeVtUnVCAd+/s/SeV0vA=";
            string TopicName = "pricechange-topic";
            string SubscriptionName = "pricechange-topic-subscription";

            subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName, ReceiveMode.PeekLock);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            // Configure the message handler options in terms of exception handling, number of concurrent messages to deliver, etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false,
                //  MaxAutoRenewDuration = TimeSpan.FromSeconds(300)
            };

            // Register the function that processes messages.
            subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

        }

        public async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            // Process the message.
            var messageBody = Encoding.UTF8.GetString(message.Body);
            var receivedMessage = JsonConvert.DeserializeObject<PriceChangeItem>(messageBody);

            UpdateCartPriceChange(receivedMessage).GetAwaiter().GetResult();

            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            // Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed.
            // If queueClient has already been closed, you can choose to not call CompleteAsync() or AbandonAsync() etc.
            // to avoid unnecessary exceptions.
        }

        public Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var exception = exceptionReceivedEventArgs.Exception;
            return Task.CompletedTask;
        }


        public async Task CloseQueueAsync()
        {
            await subscriptionClient.CloseAsync();
        }

        public async Task<int> UpdateCartPriceChange(PriceChangeItem priceChange)
        {
            string RestaurantApiUrl = "http://localhost:11789/";
            using (HttpClient httpClient = WebAPIClient.GetClient(priceChange.UserToken, priceChange.UserID, RestaurantApiUrl))
            {
                //GET Method  
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/CartItemPriceChange?RestaurantID=" + priceChange.RestaurantId + "&MenuID=" + priceChange.MenuId + "&Price=" + priceChange.Price);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string json = await httpResponseMessage.Content.ReadAsStringAsync();
                    int retVal = JsonConvert.DeserializeObject<int>(json);
                    if (retVal != 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            return -1;
        }


    }
}