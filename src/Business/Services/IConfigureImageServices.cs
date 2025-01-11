using Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Domain.DTOs.Read;

namespace Business.Services
{
    public class IConfigureImageServices : IFileManager
    {
        public async  Task<FileResponse> ConfigureImage(IFormFile file)
        {
            string photoPath;

            string photosDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "StudentPhotos");

            if (!Directory.Exists(photosDirectory))
            {
                Directory.CreateDirectory(photosDirectory);
            }

            photoPath = Path.Combine(photosDirectory, file.FileName);

            using (var stream = new FileStream(photoPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new FileResponse {   FileName= file.FileName, FilePath = photoPath };

           }
    }
}
