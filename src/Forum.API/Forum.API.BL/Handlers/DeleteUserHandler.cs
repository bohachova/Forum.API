using Forum.API.BL.Commands;
using Forum.API.DAL;
using Forum.API.DataObjects.Responses;
using MediatR;
using Forum.API.DataObjects.Enums;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        public DeleteUserHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserRole == UserRole.Admin || (request.UserRole == UserRole.User && request.UserId == request.DeletedUserId))
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.DeletedUserId);
                if (user != null && !user.DeletedUser)
                { 
                    user.DeletedUser = true;
                    await dbContext.SaveChangesAsync();
                    return new Response { IsSuccess = true };
                }
            }
            return new Response { IsSuccess = false };
        }
    }
}
