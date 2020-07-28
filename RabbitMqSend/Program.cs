using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMqSend
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                Console.WriteLine("Enter Message");

                string message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);

                while (true)
                {
                    channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                    Console.WriteLine(" [x] Sent {0}", message);

                   // Console.ReadKey();
                }
            }
        }
    }
}
