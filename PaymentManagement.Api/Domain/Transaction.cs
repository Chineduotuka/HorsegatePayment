using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManagement.Api.Domain
{
    public class Transaction
    {
        public string UserId {get; set;} 
        public string TransactionId {get; set;} = Guid.NewGuid().ToString();
        public string PaymentReference {get; set;}
        public DateTimeOffset CreatedAt {get;set;}
        public DateTimeOffset ExpiredAt {get; set;}
    }
}