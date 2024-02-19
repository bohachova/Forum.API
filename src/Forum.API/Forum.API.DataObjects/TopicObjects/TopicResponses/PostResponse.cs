using Forum.API.DataObjects.Pagination;
using Forum.API.DataObjects.UserObjects.UserResponses;

namespace Forum.API.DataObjects.TopicObjects.TopicResponses
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<AttachmentResponse> Attachments { get; set; } = new List<AttachmentResponse>();
        public DateTime PostPublishingTime { get; set; } = DateTime.Now;
        public bool WasEdited { get; set; }
        public DateTime? LastEdited { get; set; }
        public int AuthorId { get; set; }
        public UserResponse Author { get; set; }
        public int TopicId { get; set; }
        public TopicResponse Topic { get; set; }
        public PaginatedList<CommentResponse> Comments { get; set; } = new PaginatedList<CommentResponse>(new List<CommentResponse>(), 0, 0, 0);
    }
}
