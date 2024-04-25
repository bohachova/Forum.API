using Forum.API.BL.Queries;
using Forum.API.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Forum.API.DataObjects.Enums;

namespace Forum.API.BL.Handlers
{
    public class RefreshBanDataHandler : IRequestHandler<RefreshBanDataQuery, Unit>
    {
        private readonly ForumDbContext dbContext;
        public RefreshBanDataHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Unit> Handle(RefreshBanDataQuery request, CancellationToken cancellationToken)
        {
            if (request.AllUsers)
            {
                var bannedUsers = await dbContext.Users.Where(x => x.BannedUser && (x.BanType == BanType.Muted || x.BanType == BanType.TemporaryBan)).ToListAsync();
                foreach (var user in bannedUsers)
                {
                    if (user.BanTime < DateTime.Now)
                    {
                        user.BannedUser = false;
                        user.BanType = BanType.NotBanned;
                        user.BanTime = null;
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            else if (!request.AllUsers)
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
                if ((user.BanType == BanType.Muted || user.BanType == BanType.TemporaryBan) && user.BanTime < DateTime.Now)
                {
                    user.BannedUser = false;
                    user.BanType = BanType.NotBanned;
                    user.BanTime = null;
                    await dbContext.SaveChangesAsync();
                }
            }
            return Unit.Value;
        }
    }

}
