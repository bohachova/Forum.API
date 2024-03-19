

namespace Forum.API.DataObjects.TopicObjects
{
    public class LikeDislikeReactionRequest
    {
        public int TargetId { get; set; }
        public bool Like { get; set; } = false;
        public bool Dislike { get; set; } = false;
    }
}
