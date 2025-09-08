using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class ExchangeModel
    {
        public string Name { get; set; }
        public RabbbitMqExchangeType Type { get; set; }
        public string RoutingKey { get; set; }

        public ExchangeModel(string exchangeName,
                             RabbbitMqExchangeType exchangeType,
                             string exchangeRoutingKey)
        {
            Name = exchangeName;
            Type = exchangeType;
            RoutingKey = exchangeRoutingKey;
        }
    }
}
