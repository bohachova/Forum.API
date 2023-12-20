using MediatR;
using Forum.API.BL.Commands;
using Forum.API.DataObjects.Responses;
using Forum.API.DAL;
using Forum.API.DataObjects.TopicObjects;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        public DeleteCommentHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(x => x.Id == request.CommentId);
            if (request.FullDeleteAllowed)
            {
                dbContext.Comments.Remove(comment);
                await dbContext.SaveChangesAsync();
                return new Response { IsSuccess = true };
            }
            else
            {
                if(comment.AuthorId == request.UserId && !comment.ChildComments.Any())
                {
                    dbContext.Comments.Remove(comment);
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
}
