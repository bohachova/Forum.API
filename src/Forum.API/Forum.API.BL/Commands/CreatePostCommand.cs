using MediatR;
using Forum.API.DataObjects.Responses;
using Microsoft.AspNetCore.Http;

namespace Forum.API.BL.Commands
{
    public class CreatePostCommand:IRequest<Response>
    {
        public string Header { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
        public int AuthorId { get; set; }
        public int TopicId { get; set; }
    }
}
