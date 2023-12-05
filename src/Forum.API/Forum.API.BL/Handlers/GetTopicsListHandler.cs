using MediatR;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.Pagination;
using Forum.API.DataObjects.TopicObjects;
using Forum.API.DAL;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class GetTopicsListHandler : IRequestHandler<GetTopicsListQuery, PaginatedList<Topic>>
    {
        private readonly ForumDbContext dbContext;
        public GetTopicsListHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<PaginatedList<Topic>> Handle(GetTopicsListQuery request, CancellationToken cancellationToken)
        {
            var topicsCount = await dbContext.Topics.CountAsync();
            var topics = await dbContext.Topics.AsNoTracking().Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            return new PaginatedList<Topic>(topics, topicsCount, request.PageIndex, request.PageSize);
        }
    }
}
