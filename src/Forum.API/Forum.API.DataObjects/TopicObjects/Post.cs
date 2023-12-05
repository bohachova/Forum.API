using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.API.DataObjects.UserObjects;

namespace Forum.API.DataObjects.TopicObjects
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
        public List<string> Attachments { get; set; } = new List<string>();
        public DateTime PostPublishingTime { get; set; } = DateTime.Now;
        [Required]
        public int AuthorId { get; set; }
        public User Author { get; set; }
        [Required]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
