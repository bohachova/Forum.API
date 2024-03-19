

namespace Forum.API.DataObjects.TopicObjects
{
    public class Reaction
    {
        public int Id { get; set; }
        public int TargetId { get; set; }
        public bool Like { get; set; } = false;
        public bool Dislike { get; set; } = false;
        public int AuthorId { get; set; }
    }
}
