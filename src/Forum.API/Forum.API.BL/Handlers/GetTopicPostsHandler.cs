using MediatR;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.Pagination;
using Forum.API.DataObjects.TopicObjects;
using Forum.API.DAL;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class GetTopicPostsHandler : IRequestHandler<GetTopicPostsQuery, PaginatedList<Post>>
    {
        private readonly ForumDbContext dbContext;
        public GetTopicPostsHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<PaginatedList<Post>> Handle(GetTopicPostsQuery request, CancellationToken cancellationToken)
        {
            var postsCount = await dbContext.Posts.CountAsync(x => x.TopicId == request.TopicId);
            var posts = await dbContext.Posts.AsNoTracking().Where(x=> x.TopicId == request.TopicId)
                                                            .Skip((request.PageIndex - 1) * request.PageSize)
                                                            .Take(request.PageSize)
                                                            .Include(x=>x.Attachments)
                                                            .ToListAsync();
            return new PaginatedList<Post>(posts, postsCount, request.PageIndex, request.PageSize);
        }
    }
}
