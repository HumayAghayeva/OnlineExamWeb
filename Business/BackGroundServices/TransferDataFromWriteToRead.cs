using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business.Services;
namespace Business.BackGroundServices
{
    public class TransferDataFromWriteToRead : BackgroundService
    {
        private readonly ITransferDataToReadDbServices _transferDataToReadDbServices;

        public TransferDataFromWriteToRead(ITransferDataToReadDbServices transferDataToReadDbServices)
        {
            _transferDataToReadDbServices=transferDataToReadDbServices; 
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                   await  _transferDataToReadDbServices.TransferDataToReadDb(cancellationToken);    
                }
                catch (Exception ex) 
                {

                }

            }

        }
    }
}
