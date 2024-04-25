using Forum.API.DataObjects.Responses;
using MediatR;

namespace Forum.API.BL.Commands
{
    public class UnbanUserCommand : IRequest<Response>
    {
        public int UserId { get; set; }
    }
}
