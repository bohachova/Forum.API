using MediatR;
using Forum.API.BL.Commands;
using Forum.API.DataObjects.Responses;
using Forum.API.DAL;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class DeletePostHandler : IRequestHandler<DeletePostCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        public DeletePostHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId);
            if (request.FullDeleteAllowed)
            {
                dbContext.Posts.Remove(post);
                await dbContext.SaveChangesAsync();
                return new Response { IsSuccess = true };
            }
            else
            {
                if(post.AuthorId == request.UserId && !post.Comments.Any())
                {
                    dbContext.Posts.Remove(post);
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
