using Forum.API.DataObjects.UserObjects;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forum.API.DataObjects.TopicObjects
{
    public class Topic
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Post> Posts { get; set; } = new List<Post>();
        public int? AuthorId { get; set; }
        [JsonIgnore]
        public User? Author { get; set; }
    }
}
