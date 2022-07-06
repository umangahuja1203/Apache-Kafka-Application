
namespace KafkaProducer
{
    public interface IDataPublisher
    {
        Task PublishAsync(Precisely precisely);
    }
}