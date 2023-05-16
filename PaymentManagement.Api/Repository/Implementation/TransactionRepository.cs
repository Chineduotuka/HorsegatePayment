using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentManagement.Api.Domain;
using PaymentManagement.Api.Repository.Interface;

namespace PaymentManagement.Api.Repository.Implementation
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PaymentDbContext _paymentDbContext;
        private readonly DbSet<Transaction> _db;

        public TransactionRepository(PaymentDbContext paymentDbContext)
        {
            _paymentDbContext = paymentDbContext;
            _db = _paymentDbContext.Set<Transaction>();
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _db;
        }

        public async Task<IQueryable<Transaction>> GetAllAsync(Expression<Func<Transaction, bool>> expression, List<string> includes = null)
        {
            IQueryable<Transaction> query = _db;
            if (includes != null)
            {
                foreach (var property in includes)
                {
                    query = query.Include(property);
                }
            }
            return query.Where(expression);
        }

        public async Task<Transaction> GetAsync(Expression<Func<Transaction, bool>> expression, List<string> includes = null)
        {
            IQueryable<Transaction> query = _db;
            if (includes != null)
            {
                foreach (var property in includes)
                {
                    query = query.Include(property);
                }
            }
            return await query.AsNoTracking().SingleOrDefaultAsync(expression);
        }

        public async Task InsertAsync(Transaction transaction)
        {
            try
            {
                var result = await _db.AddAsync(transaction);
                await _paymentDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}