using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Postulants.Domain.Communication;
using EasyJob.API.Postulants.Domain.Models;

namespace EasyJob.API.Postulants.Domain
{
    public interface IPostulantService
    {
        Task<IEnumerable<Postulant>> ListAsync();
        Task<PostulantResponse> GetById(int id);
        Task<PostulantResponse> SaveAsync(Postulant postulant);
        Task<PostulantResponse> UpdateAsync(int id, Postulant postulant);
        Task<PostulantResponse> DeleteAsync(int id);
    }
}