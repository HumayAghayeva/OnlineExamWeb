using Abstraction.Command;
using Domain.DTOs.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Enums;
using System.Data;
using Domain.DTOs.Read;
using Azure.Core;
using static Azure.Core.HttpHeader;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Infrastructure.DataContext.Write;
using Business.Repositories;

namespace Business.Services.Write
{
    public class StudentServicesWrite :IStudentService
    {
        private readonly IStudentRepository _studentCommandRepository;
        public StudentServicesWrite(IStudentRepository studentCommandRepository)
        {
            _studentCommandRepository = studentCommandRepository;
        }

    }
}