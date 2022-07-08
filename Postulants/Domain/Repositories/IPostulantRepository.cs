using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Postulants.Domain.Models;

namespace EasyJob.API.Postulants.Domain.Repository
{
    public interface IPostulantRepository
    {
        Task<IEnumerable<Postulant>> ListAsync();
        Task<Postulant> FindById(int id);
        Task AddAsync(Postulant postulant);
        void Update(Postulant postulant);
        void Remove(Postulant postulant);
    }
}