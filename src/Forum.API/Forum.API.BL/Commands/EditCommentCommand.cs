using Forum.API.DataObjects.Responses;
using Forum.API.DataObjects.TopicObjects;
using Forum.API.DataObjects.UserObjects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.API.BL.Commands
{
    public class EditCommentCommand : IRequest<Response>
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime EditingTime { get; set; } = DateTime.Now;
    }
}
