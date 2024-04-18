using Forum.API.BL.Commands;
using Forum.API.DAL;
using Forum.API.DataObjects.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Forum.API.DataObjects.Enums;

namespace Forum.API.BL.Handlers
{
    public class UnbanUserHandler : IRequestHandler<UnbanUserCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        public UnbanUserHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response> Handle(UnbanUserCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            user.BannedUser = false;
            user.BanType = BanType.NotBanned;
            user.BanTime = null;
            await dbContext.SaveChangesAsync();
            return new Response { IsSuccess = true };
        }
    }
}
