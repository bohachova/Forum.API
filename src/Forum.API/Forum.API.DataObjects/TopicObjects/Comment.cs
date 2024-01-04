using Forum.API.DataObjects.UserObjects;
using System.ComponentModel.DataAnnotations;

namespace Forum.API.DataObjects.TopicObjects
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int ParentId { get; set; }
        public int? AuthorId { get; set; }
        public User Author { get; set; }
        [Required]
        [StringLength(500)]
        public string Text { get; set; } = string.Empty;
        public DateTime PublishingTime { get; set; } = DateTime.Now;
        public List<Comment> ChildComments { get; set; } = new List<Comment>();
        public int ParentAuthorId { get; set; }
    }
}
