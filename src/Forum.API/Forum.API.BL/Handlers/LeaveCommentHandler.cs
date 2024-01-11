using MediatR;
using Forum.API.BL.Commands;
using Forum.API.DataObjects.Responses;
using Forum.API.DAL;
using Forum.API.DataObjects.TopicObjects;

namespace Forum.API.BL.Handlers
{
    public class LeaveCommentHandler : IRequestHandler<LeaveCommentCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        public LeaveCommentHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response> Handle(LeaveCommentCommand request, CancellationToken cancellationToken)
        {
            if (request.AuthorId != request.ParentAuthorId)
            {
                var comment = new Comment { Text = request.Text, AuthorId = request.AuthorId, PostId = request.PostId, ParentId = request.ParentId };
                await dbContext.Comments.AddAsync(comment);
                await dbContext.SaveChangesAsync();
                return new Response { IsSuccess = true };
            }
            else
            {
                return new Response { IsSuccess = false };
            }
        }
    }
}
