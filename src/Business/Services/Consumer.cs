using Abstraction.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Text;
using Domain.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Business.Services
{
    public class Consumer : IConsumer
    {
        private readonly IConnectionFactory _rabbitConnectionFactory;
        private readonly IConfiguration _configuration;
        private IConnection? _connection;

        public Consumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _rabbitConnectionFactory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:Connection"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"],
                Port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672")
            };
        }

        public async Task<bool> ReceiveAsync<T>(ConsumeModel<T> consumeModel)
        {
            _connection = _rabbitConnectionFactory.CreateConnection();
            IModel channel = _connection.CreateModel();

            channel.QueueDeclare(queue: consumeModel.QueueTag,
                                   durable: consumeModel.Setting.Durable,
                                   exclusive: consumeModel.Setting.Exclusive,
                                   autoDelete: consumeModel.Setting.AutoDelete);

            channel.BasicQos(prefetchSize: consumeModel.Setting.PreFetchSize,
                             prefetchCount: consumeModel.Setting.PreFetchCount,
                             global: consumeModel.Setting.Global);

            AsyncEventingBasicConsumer consumer = new(channel);

            consumer.Received += async (sender, ea) =>
            {
                byte[] arrayBody = ea.Body.ToArray();
                string body = Encoding.UTF8.GetString(arrayBody);
                T actionInput = JsonConvert.DeserializeObject<T>(body);

                var actionResult = Task.Run(() => consumeModel.Action(actionInput));
                bool isActionSuccessfull = await actionResult;
                var channel = ((AsyncEventingBasicConsumer)sender).Model;

                if (isActionSuccessfull)
                    channel.BasicAck(deliveryTag: ea.DeliveryTag,
                                     multiple: consumeModel.Setting.Multiple);
                else
                    channel.BasicNack(deliveryTag: ea.DeliveryTag,
                                     multiple: consumeModel.Setting.Multiple,
                                     requeue: consumeModel.Setting.ReQueue);
            };

            channel.BasicConsume(queue: consumeModel.QueueTag,
                                 autoAck: consumeModel.Setting.AutoAck,
                                 consumer: consumer);

            return true;

        }
    }
}