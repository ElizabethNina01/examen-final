using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Repositories;
using EasyJob.API.Applicants.Domain.Services.Communication;
using EasyJob.API.Payments.Domain.Models;
using EasyJob.API.Payments.Domain.Repositories;
using EasyJob.API.Payments.Domain.Services;
using EasyJob.API.Payments.Domain.Services.Communication;
using EasyJob.API.Payments.Domain.Services.Communication;
using Go2Climb.API.Domain.Repositories;

namespace EasyJob.API.Payments.Services
{
    public class PaymentService : IPaymentService
    {
         private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IUnitOfWork unitOfWork, IPaymentRepository paymentRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> ListAsync()
        {
            return await _paymentRepository.ListAsync();
        }

        public async Task<PaymentResponse> GetById(int id)
        {
            var existingPayment = _paymentRepository.FindById(id);
            if (existingPayment.Result == null)
                return new PaymentResponse("The payment does not exist.");
            
            return new PaymentResponse(existingPayment.Result);
        }

        public async Task<PaymentResponse> SaveAsync(Payment payment)
        {
            try
            {
                await _paymentRepository.AddAsync(payment);
                await _unitOfWork.CompleteAsync();
                return new PaymentResponse(payment);
            }
            catch (Exception e)
            {
                return new PaymentResponse($"An error occurred while saving the payment: {e.Message}");
            }
        }

        public async Task<PaymentResponse> UpdateAsync(int id, Payment payment)
        {
            var existingPayment = await _paymentRepository.FindById(id);
            if (existingPayment == null)
                return new PaymentResponse("Payment not found");
            existingPayment.Method = payment.Method;
            try
            {
                _paymentRepository.Update(existingPayment);
                await _unitOfWork.CompleteAsync();
                return new PaymentResponse(existingPayment);
            }
            catch (Exception e)
            {
                return new PaymentResponse($"An error occurred while updating the Payment: {e.Message}");
            }
        }

        public async Task<PaymentResponse> DeleteAsync(int id)
        {
            var existingPayment = await _paymentRepository.FindById(id);
            if (existingPayment == null)
                return new PaymentResponse("Payment not found");
            try
            {
                _paymentRepository.Remove(existingPayment);
                await _unitOfWork.CompleteAsync();
                return new PaymentResponse(existingPayment);
            }
            catch (Exception e)
            {
                return new PaymentResponse($"An error occurred while deleting the Payment: {e.Message}");
            }
        }
    }
}