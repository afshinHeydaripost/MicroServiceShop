using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;

namespace Order.Api.Class;
//public class RabbitMqListener : BackgroundService
//{
//    private readonly ILogger<RabbitMqListener> _logger;
//    private readonly IServiceProvider _serviceProvider;
//    private readonly IConfiguration _config;

//    private IConnection _connection;
//    private IModel _channel;
//    private string _queueName;

//    public RabbitMqListener(
//        ILogger<RabbitMqListener> logger,
//        IServiceProvider serviceProvider,
//        IConfiguration config)
//    {
//        _logger = logger;
//        _serviceProvider = serviceProvider;
//        _config = config;

//        InitializeRabbitMq();
//    }

//    private void InitializeRabbitMq()
//    {
//        var factory = new ConnectionFactory()
//        {
//            HostName = _config["RabbitMQ:Host"] ?? "localhost",
//            UserName = _config["RabbitMQ:Username"] ?? "guest",
//            Password = _config["RabbitMQ:Password"] ?? "guest",
//            AutomaticRecoveryEnabled = true // در نسخه 7.1.2 موجود است
//        };

//        _connection = factory.CreateConnection();
//        _channel = _connection.CreateModel();

//        _queueName = _config["RabbitMQ:QueueName"] ?? "product-events";

//        _channel.QueueDeclare(
//            queue: _queueName,
//            durable: true,
//            exclusive: false,
//            autoDelete: false,
//            arguments: null
//        );

//        _logger.LogInformation($"✅ RabbitMQ Listener connected to queue: {_queueName}");
//    }

//    protected override Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        var consumer = new EventingBasicConsumer(_channel);

//        consumer.Received += (model, ea) =>
//        {
//            try
//            {
//                var body = ea.Body;
//                var message = Encoding.UTF8.GetString(body.ToArray());
//                var productEvent = JsonSerializer.Deserialize<ProductMessage>(message);

//                if (productEvent == null)
//                {
//                    _logger.LogWarning("⚠️ Received empty or invalid message");
//                    _channel.BasicAck(ea.DeliveryTag, false);
//                    return;
//                }

//                using (var scope = _serviceProvider.CreateScope())
//                {
//                    var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
//                    HandleProductEvent(productEvent, db);
//                }

//                _channel.BasicAck(ea.DeliveryTag, false);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "❌ Error processing RabbitMQ message");
//            }
//        };

//        _channel.BasicConsume(
//            queue: _queueName,
//            autoAck: false,
//            consumer: consumer
//        );

//        _logger.LogInformation("👂 Listening for product events...");
//        return Task.CompletedTask;
//    }

//    private void HandleProductEvent(ProductInfoViewModel message, OrderDbContext db)
//    {
//        switch (message.EventType)
//        {
//            case "ProductCreated":
//                if (!db.ProductCaches.Any(p => p.ProductId == message.ProductId))
//                {
//                    db.ProductCaches.Add(new ProductCache
//                    {
//                        ProductId = message.ProductId,
//                        Title = message.Title ?? "",
//                        Price = message.Price ?? 0,
//                        LastUpdated = DateTime.UtcNow
//                    });
//                    db.SaveChanges();
//                    _logger.LogInformation($"✅ Product created: {message.Title}");
//                }
//                break;

//            case "ProductUpdated":
//                var existing = db.ProductCaches.FirstOrDefault(p => p.ProductId == message.ProductId);
//                if (existing != null)
//                {
//                    existing.Title = message.Title ?? existing.Title;
//                    existing.Price = message.Price ?? existing.Price;
//                    existing.LastUpdated = DateTime.UtcNow;
//                    db.SaveChanges();
//                    _logger.LogInformation($"🔁 Product updated: {message.Title}");
//                }
//                break;

//            case "ProductDeleted":
//                var deleted = db.ProductCaches.FirstOrDefault(p => p.ProductId == message.ProductId);
//                if (deleted != null)
//                {
//                    db.ProductCaches.Remove(deleted);
//                    db.SaveChanges();
//                    _logger.LogInformation($"🗑️ Product deleted: {message.ProductId}");
//                }
//                break;

//            default:
//                _logger.LogWarning($"⚠️ Unknown EventType: {message.EventType}");
//                break;
//        }
//    }

//    public override void Dispose()
//    {
//        _channel?.Clos();
//        _connection?.Close();
//        base.Dispose();
//    }
//}



