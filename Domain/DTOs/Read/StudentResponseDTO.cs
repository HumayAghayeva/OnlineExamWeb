using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.DTOs.Read
{
    public record StudentResponseDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PIN { get; set; }
        public string CreatedTime { get; set; } 
        public string UpdatedTime { get; set; } 
        public bool IsDeleted { get; set; }
        public string? GroupName { get; set; }
        public string PhotoUrl { get; set; }    
    }
}
