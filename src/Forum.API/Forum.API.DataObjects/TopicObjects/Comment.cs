using Forum.API.DataObjects.UserObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Forum.API.DataObjects.TopicObjects
{
    public class Comment
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }
        public int? ParentId { get; set; }
        public Comment? Parent { get; set; }
        public int? AuthorId { get; set; }
        public User? Author { get; set; }
        [Required]
        [StringLength(500)]
        public string Text { get; set; } = string.Empty;
        public DateTime PublishingTime { get; set; } = DateTime.Now;
        [JsonIgnore]
        public List<Comment> ChildComments { get; set; } = new List<Comment>();
        [NotMapped]
        public bool HasChildComments { get; set; }
        public int ParentAuthorId { get; set; }
    }
}