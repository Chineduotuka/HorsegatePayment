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

    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _paymentDbContext;
        private readonly DbSet<Payment> _db;

        public PaymentRepository(PaymentDbContext paymentDbContext)
        {
            _paymentDbContext = paymentDbContext;
            _db = _paymentDbContext.Set<Payment>();
        }

        public async Task<IQueryable<Payment>> GetAllAsync(Expression<Func<Payment, bool>> expression, List<string> includes = null)
        {
            IQueryable<Payment> query = _db;
            if (includes != null)
            {
                foreach (var property in includes)
                {
                    query = query.Include(property);
                }
            }
            return query.Where(expression);
        }

        public async Task<Payment> GetAsync(Expression<Func<Payment, bool>> expression, List<string> includes = null)
        {
            IQueryable<Payment> query = _db;
            if (includes != null)
            {
                foreach (var property in includes)
                {
                    query = query.Include(property);
                }
            }
            return await query.AsNoTracking().SingleOrDefaultAsync(expression);
        }

        public async Task InsertAsync(Payment payment)
        {
            try
            {
                var result = await _db.AddAsync(payment);
                await _paymentDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}