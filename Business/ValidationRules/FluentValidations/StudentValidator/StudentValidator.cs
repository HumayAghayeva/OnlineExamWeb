using Domain.DTOs.Write;
using Domain.Entity.Read;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidations.StudentValidator
{
    public class StudentValidator : AbstractValidator<StudentRequestDTO>
    {
        public StudentValidator() 
        {
            RuleFor(x => x.Email).EmailAddress(); 
            RuleFor(x => x.PIN).Length(7); 
            RuleFor(x => x.Name).NotNull(); 
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x=>x.Password).NotNull();   
            RuleFor(x=>x.ConfirmPassword).NotNull(); 
            RuleFor(x => x.Password.CompareTo(x.ConfirmPassword));
        }   

    }
}
