using System.Collections.Generic;
using System.Threading.Tasks;using EasyJob.API.Payments.Domain.Models;
using EasyJob.API.Payments.Domain.Services.Communication;
namespace EasyJob.API.Payments.Domain.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> ListAsync();
        Task<PaymentResponse> GetById(int id);
        Task<PaymentResponse> SaveAsync(Payment payment);
        Task<PaymentResponse> UpdateAsync(int id, Payment payment);
        Task<PaymentResponse> DeleteAsync(int id);

    }
}