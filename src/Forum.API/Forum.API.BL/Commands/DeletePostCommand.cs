
using Forum.API.DataObjects.Responses;
using MediatR;


namespace Forum.API.BL.Commands
{
    public class DeletePostCommand: IRequest<Response>
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public bool FullDeleteAllowed { get; set; }
    }
}
