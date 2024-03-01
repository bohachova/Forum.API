using System.ComponentModel.DataAnnotations;
using Forum.API.DataObjects.UserObjects;

namespace Forum.API.DataObjects.TopicObjects.PostObjects
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Header { get; set; } = string.Empty;
        [Required]
        [StringLength(1000)]
        public string Text { get; set; } = string.Empty;
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        public DateTime PostPublishingTime { get; set; } = DateTime.Now;
        public bool WasEdited { get; set; }
        public DateTime? LastEdited { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public User Author { get; set; }
        [Required]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Reaction> Reactions { get; set; } = new List<Reaction>();
    }
}
