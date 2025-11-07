using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderService.Messaging;
public class MultiQueueRabbitMqListenerService : BackgroundService
{
    private readonly ILogger<MultiQueueRabbitMqListenerService> _logger;
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly IConfiguration _config;
    // ساختار نگهداری صف و callback مدل
    private readonly Dictionary<string, Func<string, Task>> _queueCallbacks;

    public MultiQueueRabbitMqListenerService(
        ILogger<MultiQueueRabbitMqListenerService> logger,
        Dictionary<string, Func<string, Task>> queueCallbacks, IConfiguration config)
    {
        _logger = logger;
        _queueCallbacks = queueCallbacks;
        _config = config;
        var factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQ:Host"],
            UserName = _config["RabbitMQ:Username"],
            Password = _config["RabbitMQ:Password"]
        };

        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        foreach (var kvp in _queueCallbacks)
        {
            var queueName = kvp.Key;
            var callback = kvp.Value;

            // تعریف صف
            await _channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (sender, ea) =>
            {
                if (stoppingToken.IsCancellationRequested) return;

                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                try
                {
                    await callback(json);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"❌ Error processing message from queue {queueName}");
                }
            };

            await _channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: true,
                consumer: consumer
            );

            _logger.LogInformation($"👂 Listening to RabbitMQ queue: {queueName}");
        }

        // BackgroundService باید تا cancellationToken صبر کنه
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null)
            await _channel.CloseAsync();
        if (_connection != null)
            await _connection.CloseAsync();

        _logger.LogInformation("🛑 Multi-queue Listener stopped.");
    }
}
