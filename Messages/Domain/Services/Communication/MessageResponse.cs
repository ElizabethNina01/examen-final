using EasyJob.API.Messages.Domain.Models;
using Go2Climb.API.Domain.Services.Communication;

namespace EasyJob.API.Messages.Services.Communication
{
    public class MessageResponse : BaseResponse<Message>
    {
        public MessageResponse(string message) : base(message)
        {
        }
        
        public MessageResponse(Message resource) : base(resource)
        {
        }  
    }
    
    
}