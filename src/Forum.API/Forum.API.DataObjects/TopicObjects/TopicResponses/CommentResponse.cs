using Forum.API.DataObjects.UserObjects.UserResponses;

namespace Forum.API.DataObjects.TopicObjects.TopicResponses
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? ParentId { get; set; }
        public CommentResponse? Parent { get; set; }
        public int? AuthorId { get; set; }
        public UserResponse? Author { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime PublishingTime { get; set; } = DateTime.Now;
        public bool HasChildComments { get; set; }
        public int ParentAuthorId { get; set; }
        public bool WasEdited { get; set; }
        public DateTime? LastEdited { get; set; }
        public List<int> Likes { get; set; } = new List<int>();
        public List<int> Dislikes { get; set; } = new List<int>();
        public List<Reaction> Reactions { get; set; } = new List<Reaction>();

    }
}
