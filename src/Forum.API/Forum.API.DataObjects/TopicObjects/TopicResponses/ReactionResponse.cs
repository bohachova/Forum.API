

namespace Forum.API.DataObjects.TopicObjects.TopicResponses
{
    public class ReactionResponse
    {
        public bool Like { get; set; } = false;
        public bool Dislike { get; set; } = false;
        public int AuthorId { get; set; }
    }
}
