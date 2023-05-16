using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PaymentManagement.Api.Domain;

namespace PaymentManagement.Api.Repository.Interface
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAll();
        Task<IQueryable<Transaction>> GetAllAsync(Expression<Func<Transaction, bool>> expression, List<string> includes = null);
        Task<Transaction> GetAsync(Expression<Func<Transaction, bool>> expression, List<string> includes = null);
        Task InsertAsync(Transaction transaction);
    }
}