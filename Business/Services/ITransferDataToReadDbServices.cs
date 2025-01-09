using Abstraction.Interfaces;
using Business.BackGroundServices;
using Domain.DTOs.Read;
using Domain.Entity.Read;
using Domain.Enums;
using Infrastructure.DataContext.Write;
using Infrastructure.Persistent.Read;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
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
        private readonly IMongoCollection<StudentResponseDTO> _studentResponseCollections;

        public ITransferDataToReadDbServices(OEPWriteDB OEPWriteDB, OEPReadDB OEPReadDB,IMongoCollection<StudentResponseDTO> studentResponseCollections)
        {
            _OEPWriteDB = OEPWriteDB;
            _OEPReadDB = OEPReadDB;
            _studentResponseCollections = studentResponseCollections;   
        }

        public async Task TransferDataFromWriteToRead(CancellationToken cancellationToken)
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
                                               PhotoPath = photo.PhotoPath,
                                               isDeleted = photo.Student.IsDeleted,
                                               CreatedDate = photo.Student.CreatedTime,
                                               UpdateDate = photo.Student.UpdatedTime
                                           })
                                           .ToList();

            if (dataWriteDb == null)
            {

                throw new ArgumentNullException(nameof(dataWriteDb), "telebe yoxdu");
            }
            else
            {
                var mongoDbStudents = new List<StudentResponseDTO>();

                foreach (var student in dataWriteDb)
                {
                    var readDBStudent = new StudentResponseDTO()
                    {
                        Name = student.Name,
                        LastName = student.LastName,
                        DateOfBirth = student.DateOfBirth,
                        PIN = student.PIN,
                        GroupName = Enum.GetName(typeof(Groups), student.GroupId),
                        Email = student.Email,
                        PhotoUrl = student.PhotoPath,
                        IsDeleted = student.isDeleted,
                        CreatedTime = student.CreatedDate.ToString(),
                        UpdatedTime = student.UpdateDate.ToString()
                    };

                    mongoDbStudents.Add(readDBStudent);

                }
                await _studentResponseCollections.InsertManyAsync(mongoDbStudents, cancellationToken: cancellationToken);

            }
        }
    }
}

