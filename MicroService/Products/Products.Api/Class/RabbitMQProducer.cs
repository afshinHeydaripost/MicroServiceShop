using Helper;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductService.Api.Class
{
    internal class RabbitMQProducer : IRabbitMQ
    {
        private readonly IConfiguration _config;

        public RabbitMQProducer(IConfiguration config)
        {
            _config = config;
        }
        public async Task<GeneralResponse> SendMessageToQueue<TRequest>(TRequest item,string queueName)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _config["RabbitMQ:Host"]
                };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false
                );

                var json = JsonSerializer.Serialize(item);
                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: queueName,
                    body: body
                );
                return GeneralResponse.Success();
            }
            catch (Exception e)
            {
                return GeneralResponse.Fail(e);
            }
        }
    }
}
