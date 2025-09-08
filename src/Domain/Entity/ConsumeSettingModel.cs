using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class ConsumeSettingModel : PublishSettingModel
    {
        public ConsumeSettingModel(bool durable = true,
                                  bool propertiesPersistance = true,
                                  bool exclusive = false,
                                  bool autoDelete = false,
                                  bool reQueue = true,
                                  ushort preFetchCount = 2,
                                  uint preFetchSize = 0,
                                  bool global = false,
                                  bool autoAck = false,
                                  bool multiple = false)
          : base(durable, propertiesPersistance, exclusive, autoDelete)
        {
            ReQueue = reQueue;
            PreFetchCount = preFetchCount;
            PreFetchSize = preFetchSize;
            Global = global;
            AutoAck = autoAck;
            Multiple = multiple;
        }

        public bool ReQueue { get; set; }
        public ushort PreFetchCount { get; set; }
        public bool Multiple { get; set; }
        public bool AutoAck { get; set; }
        public uint PreFetchSize { get; set; }
        public bool Global { get; set; }


    }
}