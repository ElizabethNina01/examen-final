using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Services.Communication;
using EasyJob.API.Messages.Domain.Models;
using EasyJob.API.Messages.Services.Communication;

namespace EasyJob.API.Messages.Services
{
    public interface IMessageServices
    {
        Task<IEnumerable<Message>> ListAsync();
        Task<MessageResponse> GetById(int id);
        Task<MessageResponse> SaveAsync(Message message);
        Task<MessageResponse> UpdateAsync(int id, Message message);
        Task<MessageResponse> DeleteAsync(int id);
        
        
    }
}