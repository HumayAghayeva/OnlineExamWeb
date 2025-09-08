using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class PublishSettingModel
    {
        public bool Durable { get; set; }
        public bool PropertiesPersistance { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="durable"></param>
        /// <param name="propertiesPersistance"></param>
        /// <param name="exclusive"></param>
        /// <param name="autoDelete"></param>
        public PublishSettingModel(bool durable = true, bool propertiesPersistance = true,
                            bool exclusive = false, bool autoDelete = false)
        {
            Durable = durable;
            PropertiesPersistance = propertiesPersistance;
            Exclusive = exclusive;
            AutoDelete = autoDelete;
        }
    }
}
