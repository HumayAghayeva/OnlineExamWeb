using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class ConsumeModel<T>
    {
        
        public Func<T, Task<bool>> Action { get; set; }

        public string QueueTag { get; set; }
        public ConsumeSettingModel Setting { get; set; }

        public ConsumeModel(string queueTag, Func<T, Task<bool>> action, ConsumeSettingModel setting = null)
        {
            Action = action;
            QueueTag = queueTag;
            Setting = setting ?? new ConsumeSettingModel();
        }
    }

}