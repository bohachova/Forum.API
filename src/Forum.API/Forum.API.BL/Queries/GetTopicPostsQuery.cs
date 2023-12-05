using MediatR;
using Forum.API.DataObjects.TopicObjects;
using Forum.API.DataObjects.Pagination;

namespace Forum.API.BL.Queries
{
    public class GetTopicPostsQuery: IRequest<PaginatedList<Post>>
    {
        public int TopicId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
