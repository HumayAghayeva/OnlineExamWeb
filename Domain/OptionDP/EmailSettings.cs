using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OptionDP
{
    public class EmailSettings
    {
        public string SenderEmail { get; set; }   
        public string Password {  get; set; }  
        public string Host {  get; set; }  
        public string? Subject {  get; set; }  
        public int Port {  get; set; }  
    }
}
