using Forum.API.DataObjects.TopicObjects.PostObjects;
using Forum.API.DataObjects.UserObjects;
using System.ComponentModel.DataAnnotations;

namespace Forum.API.DataObjects.TopicObjects
{
    public class Topic
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        public List<Post> Posts { get; set; } = new List<Post>();
        public int? AuthorId { get; set; }
        public User? Author { get; set; }
    }
}
