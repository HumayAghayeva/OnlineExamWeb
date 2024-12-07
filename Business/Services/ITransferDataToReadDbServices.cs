using Abstraction.Interfaces;
using Business.BackGroundServices;
using Domain.Entity.Read;
using Domain.Enums;
using Infrastructure.DataContext.Write;
using Infrastructure.Persistent.Read;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ITransferDataToReadDbServices : ITransferDataToReadDb
    {
        private readonly OEPWriteDB _OEPWriteDB;
        private readonly OEPReadDB _OEPReadDB;

        public ITransferDataToReadDbServices(OEPWriteDB OEPWriteDB, OEPReadDB OEPReadDB)
        {
            _OEPWriteDB = OEPWriteDB;
            _OEPReadDB = OEPReadDB;
        }

        public async Task TransferDataToReadDb(CancellationToken cancellationToken)
        {
            //var dataWriteDb = await _OEPWriteDB.Students.Where(w => w.CreatedTime == DateTime.Now.Date).ToListAsync(cancellationToken);

            var dataWriteDb = _OEPWriteDB.StudentPhotos
                                           .Include(photo => photo.Student)
                                           .Select(photo => new
                                           {
                                               StudentId = photo.Student.ID,
                                               Name = photo.Student.Name,
                                               LastName = photo.Student.LastName,
                                               DateOfBirth = photo.Student.DateOfBirth,
                                               PIN = photo.Student.PIN,
                                               GroupId = photo.Student.GroupId,
                                               Email = photo.Student.Email,
                                               PhotoFileName = photo.FileName,
                                               PhotoPath = photo.PhotoPath
                                           })
                                           .ToList();

            if (dataWriteDb == null)
            {

                throw new ArgumentNullException(nameof(dataWriteDb), "telebe yoxdu");
            }
            else
            {

                foreach (var student in dataWriteDb)
                {
                    var readDBStudent = new Student()
                    {
                        Name = student.Name,
                        LastName = student.LastName,
                        DateOfBirth = student.DateOfBirth,
                        PIN = student.PIN,
                        GroupName = Enum.GetName(typeof(Groups), student.GroupId),
                        Email = student.Email,
                        PhotoUrl = student.PhotoPath
                    };


                    await _OEPReadDB.Set<Student>().AddAsync(readDBStudent);
                }

            }
        }
    }
}

