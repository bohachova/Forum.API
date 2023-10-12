using MediatR;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.Responses;
using Forum.API.BL.Abstracts;
using Forum.API.DAL;
using Forum.API.BL.Security;

namespace Forum.API.BL.Handlers
{
    public class AuthorizationHandler : IRequestHandler<AuthorizationQuery, AuthResponse>
    {
        private readonly ForumDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMediator mediator;
        public AuthorizationHandler(ForumDbContext dbContext, IPasswordHasher passwordHasher, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.mediator = mediator;
        }
        public async Task<AuthResponse> Handle(AuthorizationQuery request, CancellationToken cancellationToken)
        {
            var query = new FindUserQuery { Email = request.Email};
            var user = await mediator.Send(query);
            if (user != null && passwordHasher.VerifyPassword(user.Password, request.Password))
            {
                var token = JWTSecurityTokenGenerator.GetToken(user.Username, user.UserRole);
                return new AuthResponse { IsSuccess = true, Message = "Authorized", JWTToken = token};
            }
            else
            {
                return new AuthResponse { IsSuccess = false, Message = "Authorization Failed"};
            }
        }
    }
}
