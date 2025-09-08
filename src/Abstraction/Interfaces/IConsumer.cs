using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces
{
    public interface IConsumer
    {
        Task<bool> ReceiveAsync<T>(ConsumeModel<T> consumeModel);
    }
}
