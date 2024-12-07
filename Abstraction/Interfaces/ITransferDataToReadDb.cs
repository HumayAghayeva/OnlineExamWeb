using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces
{
    public interface ITransferDataToReadDb
    {
        public  async Task ITransferDataFromWriteToRead(CancellationToken cancellationToken) { }   
    }
}
