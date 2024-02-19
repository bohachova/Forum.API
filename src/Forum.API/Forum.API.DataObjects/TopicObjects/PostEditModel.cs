using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Forum.API.DataObjects.TopicObjects
{
    public class PostEditModel
    {

        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Header { get; set; } = string.Empty;
        [Required]
        [StringLength(1000)]
        public string Text { get; set; } = string.Empty;
        public List<IFormFile> NewAttachments { get; set; } = new List<IFormFile>();
        public List<int> DeletedAttachments { get; set; } = new List<int>();
        public DateTime EditTime { get; set; } = DateTime.Now;
        public string DeletedAttachmentsString { get; set; } = string.Empty;
    }
}
