using Forum.API.DataObjects.Responses;
using MediatR;

namespace Forum.API.BL.Commands
{
    public class DeleteUserPhotoCommand : IRequest<Response>
    {
        public int UserId { get; set; }
    }
}
