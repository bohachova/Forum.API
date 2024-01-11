using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Forum.API.DataObjects.Pagination;
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
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        public DateTime PostPublishingTime { get; set; } = DateTime.Now;
        [Required]
        public int AuthorId { get; set; }
        public User Author { get; set; }
        [Required]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        [JsonIgnore]
        [Column("Comments")]
        public List<Comment> UnpaginatedComments { get; set; } = new List<Comment>();

        [NotMapped]
        public PaginatedList<Comment> Comments { get; set; } = new PaginatedList<Comment>(new List<Comment>(), 0, 0, 0);
    }
}
