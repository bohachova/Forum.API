using MediatR;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.UserObjects.UserResponses;
using Forum.API.DAL;
using Forum.API.DataObjects.Enums;
using Microsoft.EntityFrameworkCore;
using Forum.API.DataObjects.Pagination;
using AutoMapper;

namespace Forum.API.BL.Handlers
{
    public class GetUserListHandler : IRequestHandler<GetUserListQuery, PaginatedList<UserResponse>>
    {
        private readonly ForumDbContext dbContext;
        private readonly IMapper mapper;
        public GetUserListHandler(ForumDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public  async Task<PaginatedList<UserResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var usersCount = await dbContext.Users.CountAsync();
            var users = await dbContext.Users.AsNoTracking().Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            var usersResp = mapper.Map<List<UserResponse>>(users);
            return new PaginatedList<UserResponse>(usersResp, usersCount, request.PageIndex, request.PageSize);
        }
    }
}
