using EasyJob.API.Postulants.Domain.Models;
using Go2Climb.API.Domain.Services.Communication;

namespace EasyJob.API.Postulants.Domain.Communication
{
    public class PostulantResponse : BaseResponse<Postulant>
    {
        public PostulantResponse(string message) : base(message)
        {
        }

        public PostulantResponse(Postulant resource) : base(resource)
        {
        }
    }
}