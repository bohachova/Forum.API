using MediatR;
using Forum.API.BL.Commands;
using Forum.API.DAL;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.Responses;

namespace Forum.API.BL.Handlers
{
    public class DeleteUserPhotoHandler : IRequestHandler<DeleteUserPhotoCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        private readonly IMediator mediator;
        public DeleteUserPhotoHandler(ForumDbContext dbContext, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;

        }
        public async Task<Response> Handle(DeleteUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var query = new FindUserQuery { Id = request.UserId };
            var user = await mediator.Send(query);
            user.UserPhoto = null;
            await dbContext.SaveChangesAsync();
            return new Response { IsSuccess = true };
        }
    }
}
