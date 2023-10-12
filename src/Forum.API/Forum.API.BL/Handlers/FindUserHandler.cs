using MediatR;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.UserObjects;
using Forum.API.DAL;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class FindUserHandler : IRequestHandler<FindUserQuery, User?>
    {
        private readonly ForumDbContext dbContext;
        public FindUserHandler(ForumDbContext dbContext)
        {
               this.dbContext = dbContext;
        }
        public async Task<User?> Handle(FindUserQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Email) && !string.IsNullOrEmpty(request.Username))
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == request.Username.ToLower() || x.Email.ToLower() == request.Email.ToLower());
                return user;
            }
            else if (string.IsNullOrEmpty(request.Username))
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == request.Email.ToLower());
                return user;
            }
            else if (string.IsNullOrEmpty(request.Email))
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x=>x.Username.ToLower() == request.Username.ToLower());
                return user;
            }
            return null;
        }
    }
}
