using Domain.DTOs.Read;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces
{
    public interface  IFileManager
    {
         Task<FileResponseDto> ConfigureImage(IFormFile file);   
    }
}
