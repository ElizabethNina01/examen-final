using Go2Climb.API.Domain.Services.Communication;
using EasyJob.API.Interviews.Domain.Models;
using Ubiety.Dns.Core;

namespace EasyJob.API.Interviews.Domain.Services.Communication
{
    public class InterviewResponse :  BaseResponse<Interview>
    {
        public InterviewResponse(string message) : base(message)
        {
        }
        
        public InterviewResponse(Interview resource) : base(resource)
        {
        } 
    }
}
