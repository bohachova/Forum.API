using Forum.API.DataObjects.Responses;
using MediatR;


namespace Forum.API.BL.Commands
{
    public class DeleteCommentCommand: IRequest<Response>
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public bool FullDeleteAllowed { get; set; }
    }
}
