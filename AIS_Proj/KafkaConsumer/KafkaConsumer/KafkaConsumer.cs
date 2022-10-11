using Confluent.Kafka;

namespace KafkaConsumer
{
    public class KafkaConsumerHandler : IHostedService
    {
        private readonly string topic;
        private ILogger<KafkaConsumerHandler> _logger;

        public KafkaConsumerHandler(ILogger<KafkaConsumerHandler> logger)
        {
            topic = "Teste";
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            var builder = new ConsumerBuilder<Ignore, string>(conf).Build();
            builder.Subscribe(topic);
            var cancelToken = new CancellationTokenSource();
            new Thread(async () => await Subscribe(cancellationToken, builder)).Start();

            await Task.CompletedTask;
            return;
        }
        public Task StopAsync (CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Subscribe(CancellationToken cancellationToken, IConsumer<Ignore, string> builder)
        {
            try
            {
                while (true)
                {
                    var jsonRecord = builder.Consume(cancellationToken).Value;
                    _logger.LogInformation(jsonRecord);
                }
            }   
            catch(Exception)
            {
                builder.Close();
            }
        }


    }
}
