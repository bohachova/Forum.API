

using System.ComponentModel.DataAnnotations;

namespace Forum.API.DataObjects.TopicObjects
{
    public class CommentEditModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Text { get; set; } = string.Empty;
        public DateTime EditTime { get; set; } = DateTime.Now;
    }
}
