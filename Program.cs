using Confluent.Kafka;
using Newtonsoft.Json;
using RealConsumer;

 
var config = new ConsumerConfig
{
    GroupId = "abcd-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest

};

// Scene-1 : 
bool FirstRun = true;
int lastOffsetCommited = 0;


using var consumer = new ConsumerBuilder<Null, string>(config).Build();

consumer.Subscribe("abcd");

CancellationTokenSource token = new();



try
{
    while (true)
    {
        
        var response = consumer.Consume(token.Token);

        //Scene-2 : 
        if (FirstRun==true)
        lastOffsetCommited = int.Parse(response.TopicPartitionOffset.Offset.ToString()) - 1;
        int currentOffset =  int.Parse(response.TopicPartitionOffset.Offset.ToString());


        if (response.Message != null)
        {
            Console.WriteLine("Partition: " + response.Partition.ToString() + " Topic-Name : " + response.Topic.ToString());
            var precisely = JsonConvert.DeserializeObject<Precisely>
                (response.Message.Value);


            
            Console.WriteLine("Last Offset : " + response.TopicPartitionOffset.Offset.ToString());

            Console.WriteLine($"Department :  {precisely?.Department} , " + $"No of Employees : {precisely?.Employees}");

            using (var context = new PreciselyContext())
            {

                var std = new RealConsumer.Precisely()
                {
                   
                    Department = precisely?.Department,
                    Employees = precisely.Employees

                };

                //Scene-3 : (only if condition)
                if (lastOffsetCommited != currentOffset)
                {
                    context.Precisely.Add(std);
                    context.SaveChanges();
                }
            }
        }

        //Sccene-4:
        FirstRun = false;
        lastOffsetCommited = currentOffset;
    }
}
catch (Exception)
{

    throw;
}

public record Precisely(int Id , string Department, int Employees);
