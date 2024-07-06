using Confluent.Kafka;
using Kafka.Producer.API.Model;
using System.Text.Json;

namespace Kafka.Producer.API.Services
{
    public class ProducerService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ProducerConfig _producerConfig;

        public ProducerService(
            ILogger<ProducerService> logger,
            IConfiguration configuration
            )
        {
            _logger = logger;
            _configuration = configuration;

            var bootstrap = _configuration.GetSection("KafkaConfig")
                .GetSection("BootstrapServer").Value;

            _producerConfig = new ProducerConfig()
            {
                BootstrapServers = bootstrap
            };
        }

        public async Task<string> SendMessage(BookDTO book)
        {
            try
            {
                _logger.LogInformation("starting to publish");

                var topic = _configuration.GetSection("KafkaConfig").GetSection("TopicName").Value;

                using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
                {
                    var message = JsonSerializer.Serialize(book);
                    var result = await producer.ProduceAsync(topic: topic, new() { Value = message });
                    _logger.LogInformation("Published");

                    return result.Status.ToString() + " - " + message;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while publish message: EX: {Exception}", ex.Message);
                throw;
            }
        }

    }
}
