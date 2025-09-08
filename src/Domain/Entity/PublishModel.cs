using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class PublishModel
    {
        public string QueueTag { get; set; }
        public byte[] Body { get; set; }
        public ExchangeModel Exchange { get; set; }
        public PublishSettingModel Setting { get; set; }

        public PublishModel(string queueTag, object body, PublishSettingModel setting = null, ExchangeModel exchange = null)
        {
            QueueTag = queueTag;
            Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
            Setting = setting ?? new PublishSettingModel();
            Exchange = exchange;
        }
    }
}
