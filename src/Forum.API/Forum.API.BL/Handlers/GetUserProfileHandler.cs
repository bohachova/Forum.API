using AutoMapper;
using Forum.API.BL.Queries;
using Forum.API.DAL;
using Forum.API.DataObjects.UserObjects.UserResponses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, UserResponse>
    {
        private readonly ForumDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public GetUserProfileHandler(ForumDbContext dbContext, IMapper mapper, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.mediator = mediator;
        }
        public async Task<UserResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var query = new RefreshBanDataQuery { AllUsers = false, UserId = request.Id };
            await mediator.Send(query);
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            return mapper.Map<UserResponse>(user);
        }
    }
}
