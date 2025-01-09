using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Services;
using Abstraction.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BackGroundServices
{
    public class TransferDataFromWriteToRead : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider; 

        public TransferDataFromWriteToRead(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider; 
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {

                        ITransferDataToReadDb transferDataToRead=scope.ServiceProvider.GetRequiredService<ITransferDataToReadDb>();
                        await transferDataToRead.TransferDataFromWriteToRead(cancellationToken);

                    }

                    // Optional delay to control the execution frequency
                    await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
                }
                catch (Exception ex)
                {
                    // Log the exception (use a logging library like Serilog or NLog)
                    // e.g., _logger.LogError(ex, "An error occurred while transferring data.");
                }
            }
        }
    }
}
