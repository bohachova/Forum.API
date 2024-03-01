using Forum.API.DataObjects.Responses;
using MediatR;

namespace Forum.API.BL.Commands
{
    public class LikeDislikePostCommand: IRequest<ReactionsResponse>
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public bool Like { get; set; } = false;
        public bool Dislike { get; set; } = false;
    }
}
