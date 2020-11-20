using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace ServiceBusPublisher
{
    class Program
    {
        const string ServiceBusConnectionString = "";
        const string TopicPath = "SM-Test";
        static TopicClient topicClient;
        static async Task Main(string[] args)
        {
            var numberOfMessages = 10;
            topicClient = new TopicClient(ServiceBusConnectionString, TopicPath);

            await SendMessagesAsync(numberOfMessages);
            await topicClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessages)
        {
            try
            {
                for (int i = 0; i < numberOfMessages; i++)
                {
                    var messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    message.UserProperties.Add("IsEven", i % 2 == 0);
                    await topicClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error sending message. Exception: {exception.Message}");
            }

        }
    }
}
