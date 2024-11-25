using Abstraction.Interfaces;
using Business.BackGroundServices;
using Infrastructure.DataContext.Write;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ITransferDataToReadDbServices: ITransferDataFromWriteToRead
    {
        private readonly OEPWriteDB _OEPWriteDB;

        public ITransferDataToReadDbServices(OEPWriteDB OEPWriteDB)
        {
            _OEPWriteDB = OEPWriteDB;
        }

        public async Task TransferDataToReadDb(CancellationToken cancellationToken)
        {
            //  var dataWriteDb = await _OEPWriteDB.Students.Where(w => w.CreatedTime == DateTime.Now.Date).ToListAsync(cancellationToken);
            var dataWriteDb = await _OEPWriteDB.Students.ToListAsync(cancellationToken);

        }
    }
}
