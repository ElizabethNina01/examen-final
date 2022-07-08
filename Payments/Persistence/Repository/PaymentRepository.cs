
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Payments.Domain.Models;
using EasyJob.API.Payments.Domain.Repositories;
using Go2Climb.API.Persistence.Contexts;
using Go2Climb.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyJob.API.Payments.Persistence.Repository
{
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<Payment>> ListAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> FindById(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);        }

        public void Update(Payment payment)
        {
            _context.Payments.Update(payment);
        }

        public void Remove(Payment payment)
        {
            _context.Payments.Remove(payment);
        }
    }
}