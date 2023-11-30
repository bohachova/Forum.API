using MediatR;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.UserObjects;
using Forum.API.DAL;
using Forum.API.DataObjects.Enums;
using Microsoft.EntityFrameworkCore;
using Forum.API.DataObjects.Pagination;

namespace Forum.API.BL.Handlers
{
    public class GetUserListHandler : IRequestHandler<GetUserListQuery, PaginatedList<User>>
    {
        private readonly ForumDbContext dbContext;
        public GetUserListHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public  async Task<PaginatedList<User>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var usersCount = await dbContext.Users.CountAsync();
            var users = await dbContext.Users.AsNoTracking().Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            foreach(var user in users)
            {
                user.Password = "";
            }
            return new PaginatedList<User>(users, usersCount, request.PageIndex, request.PageSize);
        }
    }
}
