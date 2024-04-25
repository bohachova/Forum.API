using Forum.API.BL.Commands;
using Forum.API.DAL;
using Forum.API.DataObjects.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class BanUserHandler : IRequestHandler<BanUserCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        public BanUserHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            user.BannedUser = true;
            user.BanType = request.BanType;
            if (request.BanTime != null)
                user.BanTime = DateTime.Now.Add((TimeSpan)request.BanTime);
            await dbContext.SaveChangesAsync();
            return new Response { IsSuccess = true };
        }
    }
}
