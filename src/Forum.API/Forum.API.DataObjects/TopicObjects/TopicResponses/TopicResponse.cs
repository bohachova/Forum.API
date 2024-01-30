

namespace Forum.API.DataObjects.TopicObjects.TopicResponses
{
    public class TopicResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int? AuthorId { get; set; }
    }
}
