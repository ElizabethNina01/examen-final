using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Payments.Domain.Models;

namespace EasyJob.API.Payments.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> ListAsync();
        Task<Payment> FindById(int id);
        Task AddAsync(Payment payment);
        void Update(Payment payment);
        void Remove(Payment payment);
    }
}