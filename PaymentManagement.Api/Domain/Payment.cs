using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManagement.Api.Domain
{
    public class Payment
    {
        public string Id {get; set;} = Guid.NewGuid().ToString();
        public int Amount {get; set;}
        public string? Email {get; set;}
        public string? CallBackUrl {get; set;}
        public DateTimeOffset CreatedAt {get; set;}
    }
}