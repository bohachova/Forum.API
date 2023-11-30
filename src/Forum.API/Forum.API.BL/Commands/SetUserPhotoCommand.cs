using MediatR;
using Forum.API.DataObjects.Responses;
using Microsoft.AspNetCore.Http;

namespace Forum.API.BL.Commands
{
    public class SetUserPhotoCommand: IRequest<Response>
    {
        public int UserId { get; set; }
        public IFormFile? File { get; set; }
    }
}
