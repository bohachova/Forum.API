using MediatR;
using Forum.API.BL.Commands;
using Forum.API.DAL;
using Forum.API.BL.Abstracts;
using Forum.API.DataObjects.Responses;
using Forum.API.BL.Security;
using Forum.API.DataObjects.Enums;
using Forum.API.BL.Queries;

namespace Forum.API.BL.Handlers
{
    public class SetAdminPasswordHandler : IRequestHandler<SetAdminPasswordCommand, AuthResponse>
    {
        private readonly ForumDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMediator mediator;
        public SetAdminPasswordHandler(ForumDbContext dbContext, IPasswordHasher passwordHasher, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.mediator = mediator;
        }

        public async Task<AuthResponse> Handle(SetAdminPasswordCommand request, CancellationToken cancellationToken)
        {
            var query = new FindUserQuery { Email = request.Email };
            var admin = await mediator.Send(query);
            var passwordHash = passwordHasher.HashPassword(request.Password);
            admin!.Password = passwordHash;
            admin!.PasswordSetRequired = false;
            await dbContext.SaveChangesAsync();
            var token = JWTSecurityTokenGenerator.GetToken(admin.Username, UserRole.Admin);
            return new AuthResponse { IsSuccess = true, Message = "Authorized", JWTToken = token};
        }
    }
}
