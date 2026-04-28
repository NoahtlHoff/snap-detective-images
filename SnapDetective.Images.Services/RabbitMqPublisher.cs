using RabbitMQ.Client;
using SnapDetective.Images.Interfaces;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

public class RabbitMqPublisher(IConfiguration config) : IMessagePublisher, IAsyncDisposable
{
    private IConnection? _connection;
    private IChannel? _channel;

    private async Task<IChannel> GetChannelAsync()
    {
        if (_channel is not null) return _channel;

        var factory = new ConnectionFactory
        {
            HostName = config["RabbitMQ:Host"] ?? "localhost",
            UserName = config["RabbitMQ:Username"] ?? "guest",
            Password = config["RabbitMQ:Password"] ?? "guest"
        };
        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();
        return _channel;
    }

    public async Task PublishAsync<T>(T message, string routingKey)
    {
        var channel = await GetChannelAsync();
        await channel.ExchangeDeclareAsync("snap-detective", ExchangeType.Topic, durable: true);
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        await channel.BasicPublishAsync("snap-detective", routingKey, body);
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null) await _channel.DisposeAsync();
        if (_connection is not null) await _connection.DisposeAsync();
    }
}