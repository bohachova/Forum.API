using MediatR;
using Forum.API.DataObjects.Responses;

namespace Forum.API.BL.Commands
{
    public class CreateTopicCommand: IRequest<Response>
    {
        public string Name { get; set; } = string.Empty;
        public int AuthorId { get; set; }
    }
}
