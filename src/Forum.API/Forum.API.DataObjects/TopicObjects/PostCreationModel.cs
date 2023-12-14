
using Forum.API.DataObjects.UserObjects;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Forum.API.DataObjects.TopicObjects
{
    public class PostCreationModel 
    {
        [Required]
        [StringLength(150)]
        public string Header { get; set; } = string.Empty;
        [Required]
        [StringLength(1000)]
        public string Text { get; set; } = string.Empty;
        public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int TopicId { get; set; }
    }
}
