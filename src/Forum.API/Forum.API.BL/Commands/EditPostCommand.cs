using Microsoft.AspNetCore.Http;
using MediatR;
using Forum.API.DataObjects.Responses;

namespace Forum.API.BL.Commands
{
    public class EditPostCommand : IRequest<Response>
    {
        public int Id { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<IFormFile> NewAttachments { get; set; } = new List<IFormFile>();
        public List<int> DeletedAttachments { get; set; } = new List<int>();
        public DateTime EditTime { get; set; } = DateTime.Now;
        public int UserId { get; set; }
    }
}
