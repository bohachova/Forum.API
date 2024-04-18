using Forum.API.DataObjects.Enums;
using Forum.API.DataObjects.Responses;
using MediatR;

namespace Forum.API.BL.Commands
{
    public class BanUserCommand: IRequest<Response>
    {
        public int UserId { get; set; }
        public BanType BanType { get; set; }
        public TimeSpan? BanTime { get; set; } = null;
    }
}
