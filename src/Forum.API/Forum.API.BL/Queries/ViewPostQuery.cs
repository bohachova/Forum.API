using Forum.API.DataObjects.TopicObjects;
using MediatR;

namespace Forum.API.BL.Queries
{
    public class ViewPostQuery : IRequest<Post>
    {
        public int PostId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
