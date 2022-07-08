using EasyJob.API.Applicants.Domain.Models;
using Go2Climb.API.Domain.Services.Communication;

namespace EasyJob.API.Applicants.Domain.Services.Communication
{
    public class ApplicantResponse : BaseResponse<Applicant>
    {
        public ApplicantResponse(string message) : base(message)
        {
        }

        public ApplicantResponse(Applicant resource) : base(resource)
        {
        }
    }
}