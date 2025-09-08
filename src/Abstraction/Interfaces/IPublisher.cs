using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces
{
    public interface IPublisher
    {
        Task<bool> SendAsync(PublishModel publishModel);
    }
}
