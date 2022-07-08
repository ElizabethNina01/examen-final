using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Interviews.Domain.Models;

namespace EasyJob.API.Interviews.Domain.Repositories
{
    public interface IInterviewsRepository
    {
        Task<IEnumerable<Interview>> ListAsync();
        Task<Interview> FindById(int id);
        Task AddAsync(Interview applicant);
        void Update(Interview applicant);
        void Remove(Interview applicant);
    }
}