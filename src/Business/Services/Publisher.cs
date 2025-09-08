using Abstraction.Interfaces;
using Domain.Entity;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class Publisher : IPublisher
    {
        private readonly IConnectionFactory _rabbitConnectionFactory;
        private readonly IConfiguration _configuration;

        public Publisher(IConfiguration configuration, IConnectionFactory rabbitConnectionFactory)
        {
            _configuration = configuration;
            _rabbitConnectionFactory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:Connection"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"],
                Port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672"),
                DispatchConsumersAsync = true
            };
        }
        public async Task<bool> SendAsync(PublishModel publishModel)
        {
            using (IConnection connection = _rabbitConnectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    if (publishModel.Exchange is not null)
                        channel.ExchangeDeclare(publishModel.Exchange.Name, publishModel.Exchange.Type.ToString(),
                                                publishModel.Setting.Durable, publishModel.Setting.AutoDelete,
                                                null);

                    channel.QueueDeclare(queue: publishModel.QueueTag,
                                                durable: publishModel.Setting.Durable,
                                                exclusive: publishModel.Setting.Exclusive,
                                                autoDelete: publishModel.Setting.AutoDelete);

                    IBasicProperties properties = null;
                    if (publishModel.Setting.PropertiesPersistance)
                    {
                        // in case of data safefty it write the messages on disk
                        properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                    }

                    channel.BasicPublish(exchange: publishModel.Exchange?.Name ?? "", routingKey: publishModel.Exchange?.RoutingKey ?? "",
                                         basicProperties: properties, body: publishModel.Body);
                }
            }

            return true;
        }
    }
}