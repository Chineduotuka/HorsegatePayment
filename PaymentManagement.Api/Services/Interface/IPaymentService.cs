using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentManagement.Api.Services.Dtos;

namespace PaymentManagement.Api.Services.Interface
{
    public interface IPaymentService
    {
         Task<string> CreatePaymentAsync(string UserId, PaymentDto paymentDto);
         Task<string> VerifyPaymentAsync(string reference);
    }
}