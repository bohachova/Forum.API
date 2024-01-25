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
        public GetUserProfileHandler(ForumDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<UserResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            return mapper.Map<UserResponse>(user);
        }
    }
}
