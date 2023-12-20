using Forum.API.DataObjects.Responses;
using MediatR;

namespace Forum.API.BL.Commands
{
    public class LeaveCommentCommand : IRequest<Response>
    {
        public int PostId { get; set; }
        public int ParentId { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; } = string.Empty;
        public int ParentAuthorId { get; set; }
    }
}
