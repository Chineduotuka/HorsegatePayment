using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PaymentManagement.Api.Domain;

namespace PaymentManagement.Api.Repository.Interface
{
    public interface IPaymentRepository
    {
         Task<IQueryable<Payment>> GetAllAsync(Expression<Func<Payment, bool>> expression, List<string> includes = null);
        Task<Payment> GetAsync(Expression<Func<Payment, bool>> expression, List<string> includes = null);
        Task InsertAsync(Payment payment);
    }
}