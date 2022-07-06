using Confluent.Kafka;
using Newtonsoft.Json;

namespace KafkaProducer
{
    public class DataPublisher : IDataPublisher
    {
        private readonly IProducer<Null, string> producer;

        public DataPublisher(IProducer<Null, string> producer)
        {
            this.producer = producer;
        }

        public async Task PublishAsync(Precisely precisely) => await producer.ProduceAsync("abcd",

            new Message<Null, string>
            {
                Value = JsonConvert.SerializeObject(precisely)
            }
            );

    }
    public record Precisely(int Id ,string department , int Employees);
}
