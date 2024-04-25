using Forum.API.DataObjects.Enums;
using Forum.API.DataObjects.Responses;
using MediatR;

namespace Forum.API.BL.Commands
{
    public class DeleteUserCommand : IRequest<Response>
    {
        public int UserId { get; set; }
        public UserRole UserRole { get; set; }
        public int DeletedUserId { get; set; }
    }
}
