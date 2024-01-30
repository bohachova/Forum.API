using MediatR;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.Pagination;
using Forum.API.DataObjects.TopicObjects.TopicResponses;
using Forum.API.DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Forum.API.BL.Handlers
{
    public class GetTopicsListHandler : IRequestHandler<GetTopicsListQuery, PaginatedList<TopicResponse>>
    {
        private readonly ForumDbContext dbContext;
        private readonly IMapper mapper;
        public GetTopicsListHandler(ForumDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<PaginatedList<TopicResponse>> Handle(GetTopicsListQuery request, CancellationToken cancellationToken)
        {
            var topicsCount = await dbContext.Topics.CountAsync();
            var topics = await dbContext.Topics.AsNoTracking().Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            var topicsResp = mapper.Map<List<TopicResponse>>(topics);
            return new PaginatedList<TopicResponse>(topicsResp, topicsCount, request.PageIndex, request.PageSize);
        }
    }
}
