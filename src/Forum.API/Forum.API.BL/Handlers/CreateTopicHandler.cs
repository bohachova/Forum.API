using Forum.API.BL.Commands;
using MediatR;
using Forum.API.DataObjects.Responses;
using Forum.API.DAL;
using Forum.API.DataObjects.TopicObjects;

namespace Forum.API.BL.Handlers
{
    public class CreateTopicHandler : IRequestHandler<CreateTopicCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        public CreateTopicHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = new Topic { Name = request.Name, AuthorId = request.AuthorId };
            await dbContext.Topics.AddAsync(topic);
            await dbContext.SaveChangesAsync();
            return new Response { IsSuccess = true };
        }
    }
}
