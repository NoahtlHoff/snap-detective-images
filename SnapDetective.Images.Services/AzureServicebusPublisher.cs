using Azure.Messaging.ServiceBus;
using SnapDetective.Images.Interfaces;
using System.Text.Json;

namespace SnapDetective.Images.Services;

public class AzureServiceBusPublisher(ServiceBusClient client) : IMessagePublisher, IAsyncDisposable
{
    public async Task PublishAsync<T>(T message, string routingKey)
    {
        await using var sender = client.CreateSender(routingKey);
        var body = JsonSerializer.Serialize(message);
        await sender.SendMessageAsync(new ServiceBusMessage(body));
    }

    public async ValueTask DisposeAsync() => await client.DisposeAsync();
}