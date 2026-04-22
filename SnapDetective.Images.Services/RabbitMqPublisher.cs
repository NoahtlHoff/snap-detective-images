using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using SnapDetective.Images.Interfaces;

namespace SnapDetective.Images.Services;

public class RabbitMqPublisher(IConnection connection) : IMessagePublisher, IAsyncDisposable
{
    private readonly IChannel _channel = connection.CreateChannelAsync().GetAwaiter().GetResult();

    public async Task PublishAsync<T>(T message, string routingKey)
    {
        await _channel.ExchangeDeclareAsync(
            exchange: "snap-detective",
            type: ExchangeType.Topic,
            durable: true);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        await _channel.BasicPublishAsync(
            exchange: "snap-detective",
            routingKey: routingKey,
            body: body);
    }

    public async ValueTask DisposeAsync() => await _channel.DisposeAsync();
}