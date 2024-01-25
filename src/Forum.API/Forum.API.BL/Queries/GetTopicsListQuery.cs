using MediatR;
using Forum.API.DataObjects.TopicObjects.TopicResponses;
using Forum.API.DataObjects.Pagination;

namespace Forum.API.BL.Queries
{
    public class GetTopicsListQuery: IRequest<PaginatedList<TopicResponse>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
