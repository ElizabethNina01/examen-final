using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;

namespace EasyJob.API.Applicants.Domain.Repositories
{
    public interface IApplicantRepository
    {
        Task<IEnumerable<Applicant>> ListAsync();
        Task<Applicant> FindById(int id);
        Task AddAsync(Applicant applicant);
        void Update(Applicant applicant);
        void Remove(Applicant applicant);
    }
}