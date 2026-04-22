namespace SnapDetective.Images.Interfaces;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, string routingKey);
}