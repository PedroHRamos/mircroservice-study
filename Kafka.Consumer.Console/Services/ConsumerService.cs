using Confluent.Kafka;
using Kafka.Consumer.Console.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kafka.Consumer.Console.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ConsumerConfig _consumerConfig;
        private readonly ParametersModel _parameters;

        public ConsumerService(ILogger<ConsumerService> logger)
        {

            _logger = logger;
            _parameters = new ParametersModel();

            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = _parameters.BootstrapServer,
                GroupId = _parameters.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();

        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            try
            {
                _logger.LogInformation("Starting to consume Message");

                _consumer.Subscribe(_parameters.TopicName);

                while(!stoppingToken.IsCancellationRequested)
                {
                    await Task.Run(() =>
                    {
                        var result = _consumer.Consume(stoppingToken);
                        var book = JsonSerializer.Deserialize<BookDTO>(result.Message.Value);
                        _logger.LogInformation("GroupId: {GroupId}, Message: {Message}", _parameters.GroupId, result.Message.Value);
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while consuming Message");
                throw;
            }
            finally
            {
                _logger.LogInformation("Consumer Stoped");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            _logger.LogInformation("Consumer stopped, connection closed");
            return Task.CompletedTask;
        }
    }
}
