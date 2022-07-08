using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Messages.Domain.Models;


namespace EasyJob.API.Messages.Domain.Repositories
{
    public interface IMessagesRepository
    {
        Task<IEnumerable<Message>> ListAsync();
        Task<Message> FindById(int id);
        Task AddAsync(Message applicant);
        void Update(Message applicant);
        void Remove(Message applicant);
    }
}