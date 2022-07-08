using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Payments.Domain.Models;
using Go2Climb.API.Domain.Services.Communication;

namespace EasyJob.API.Payments.Domain.Services.Communication
{
    public class PaymentResponse: BaseResponse<Payment>

    {
        public PaymentResponse(string message) : base(message)
        {
        }

        public PaymentResponse(Payment resource) : base(resource)
        {
        }
    }
}