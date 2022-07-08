using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Services.Communication;
using EasyJob.API.Interviews.Domain.Models;
using EasyJob.API.Interviews.Domain.Services.Communication;

namespace EasyJob.API.Interviews.Domain.Services
{
    public interface IInterviewServices
    {
        Task<IEnumerable<Interview>> ListAsync();
        Task<InterviewResponse> GetById(int id);
        Task<InterviewResponse> SaveAsync(Interview interview);
        Task<InterviewResponse> UpdateAsync(int id, Interview interview);
        Task<InterviewResponse> DeleteAsync(int id);
    }
}