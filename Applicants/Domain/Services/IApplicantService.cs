using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Services.Communication;

namespace EasyJob.API.Applicants.Domain.Services
{
    public interface IApplicantService
    {
        Task<IEnumerable<Applicant>> ListAsync();
        Task<ApplicantResponse> GetById(int id);
        Task<ApplicantResponse> SaveAsync(Applicant applicant);
        Task<ApplicantResponse> UpdateAsync(int id, Applicant applicant);
        Task<ApplicantResponse> DeleteAsync(int id);
    }
}