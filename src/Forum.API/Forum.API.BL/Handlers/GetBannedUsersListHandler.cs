using AutoMapper;
using Forum.API.BL.Queries;
using Forum.API.DAL;
using Forum.API.DataObjects.Pagination;
using Forum.API.DataObjects.UserObjects.UserResponses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Forum.API.BL.Handlers
{
    public class GetBannedUsersListHandler : IRequestHandler<GetBannedUsersListQuery, PaginatedList<UserResponse>>
    {
        private readonly ForumDbContext dbContext;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public GetBannedUsersListHandler(ForumDbContext dbContext, IMediator mediator, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.mapper = mapper;
        }
        public async Task<PaginatedList<UserResponse>> Handle(GetBannedUsersListQuery request, CancellationToken cancellationToken)
        {
            var query = new RefreshBanDataQuery { AllUsers = true };
            await mediator.Send(query);
            var usersCount = await dbContext.Users.Where(x => x.BannedUser).CountAsync();
            var bannedUsers = await dbContext.Users.Where(x => x.BannedUser)
                                                    .AsNoTracking()
                                                    .Skip((request.PageIndex - 1))
                                                    .Take(request.PageSize)
                                                    .ToListAsync();
            var usersResp = mapper.Map<List<UserResponse>>(bannedUsers);
            return new PaginatedList<UserResponse>(usersResp, usersCount, request.PageIndex, request.PageSize);
        }
    }
}
