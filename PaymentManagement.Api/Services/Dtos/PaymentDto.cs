using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManagement.Api.Services.Dtos
{
    public class PaymentDto
    {
        public string Amount {get; set;}
        public string Email {get; set;}
        public string? CallBackUrl {get; set;}
        
    }
}