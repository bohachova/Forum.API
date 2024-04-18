using MediatR;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.Responses;
using Forum.API.BL.Abstracts;
using Forum.API.DAL;
using Forum.API.BL.Security;
using Forum.API.DataObjects.Enums;

namespace Forum.API.BL.Handlers
{
    public class AuthorizationHandler : IRequestHandler<AuthorizationQuery, AuthResponse>
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly IMediator mediator;
        public AuthorizationHandler(IPasswordHasher passwordHasher, IMediator mediator)
        {
            this.passwordHasher = passwordHasher;
            this.mediator = mediator;
        }
        public async Task<AuthResponse> Handle(AuthorizationQuery request, CancellationToken cancellationToken)
        {
            var query = new FindUserQuery { Email = request.Email};
            var user = await mediator.Send(query);
            if (user != null && !user.DeletedUser && passwordHasher.VerifyPassword(request.Password, user.Password))
            {
                if (user.BannedUser)
                {
                    switch (user.BanType)
                    {
                        case BanType.PermanentBan:
                            return new AuthResponse { IsSuccess = false, Message = "Ypu are on permanent ban" };
                        case BanType.TemporaryBan:
                            return new AuthResponse { IsSuccess = false, Message = $"You are banned until {user.BanTime.Value.ToString("dd.MM.yyyy")} " };
                        case BanType.Muted:
                            var tokenMuted = JWTSecurityTokenGenerator.GetToken(user.Id, user.Username, user.UserRole, true);
                            return new AuthResponse { IsSuccess = true, Message = "Authorized", JWTToken = tokenMuted };
                        default:
                            return new AuthResponse { IsSuccess = false };
                    }
                }
                var token = JWTSecurityTokenGenerator.GetToken(user.Id, user.Username, user.UserRole);
                return new AuthResponse { IsSuccess = true, Message = "Authorized", JWTToken = token};
            }
            else
            {
                return new AuthResponse { IsSuccess = false, Message = "Authorization Failed"};
            }
        }
    }
}
