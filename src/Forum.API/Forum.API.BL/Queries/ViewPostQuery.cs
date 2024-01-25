using Forum.API.DataObjects.TopicObjects.TopicResponses;
using MediatR;

namespace Forum.API.BL.Queries
{
    public class ViewPostQuery : IRequest<PostResponse>
    {
        public int PostId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
