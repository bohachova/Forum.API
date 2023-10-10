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
        public AuthorizationHandler(ForumDbContext dbContext, IPasswordHasher passwordHasher)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }
        public Task<AuthResponse> Handle(AuthorizationQuery request, CancellationToken cancellationToken)
        {
            var user = dbContext.Users.FirstOrDefault(x=>x.Email == request.Email);
            if (user != null && passwordHasher.VerifyPassword(user.Password, request.Password))
            {
                var token = JWTSecurityTokenGenerator.GetToken(user.Username, user.UserRole);
                return Task.FromResult(new AuthResponse { IsSuccess = true, Message = "Authorized", JWTToken = token});
            }
            else
            {
                return Task.FromResult(new AuthResponse { IsSuccess = false, Message = "Authorization Failed"});
            }
        }
    }
}
