using Forum.API.BL.Queries;
using Forum.API.DAL;
using Forum.API.DataObjects.Pagination;
using Forum.API.DataObjects.TopicObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class ViewPostHandler : IRequestHandler<ViewPostQuery, Post>
    {
        private readonly ForumDbContext dbContext;
        public ViewPostHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Post> Handle(ViewPostQuery request, CancellationToken cancellationToken)
        {
            var post = await dbContext.Posts.Where(x=> x.Id == request.PostId).Include(x=>x.Author).FirstOrDefaultAsync();
            var comments = await dbContext.Comments.Where(x => x.PostId == request.PostId)
                                                   .Include(x => x.Author)
                                                   .Include(x => x.Parent)
                                                   .Skip((request.PageIndex - 1) * request.PageSize)
                                                   .Take(request.PageSize)
                                                   .ToListAsync();
            foreach(var comment in comments )
            {
                comment.HasChildComments = await dbContext.Comments.Where(x => x.ParentId == comment.Id).AnyAsync();
            }
            PaginatedList<Comment> paginatedComments = new PaginatedList<Comment> (comments, comments.Count, request.PageIndex, request.PageSize);
            post.Comments = paginatedComments;
            return post;
        }
    }
}
