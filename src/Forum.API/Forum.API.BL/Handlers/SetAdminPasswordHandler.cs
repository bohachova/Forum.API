using MediatR;
using Forum.API.BL.Commands;
using Forum.API.DAL;
using Forum.API.BL.Abstracts;
using Forum.API.DataObjects.Responses;
using Forum.API.BL.Security;
using Forum.API.DataObjects.Enums;

namespace Forum.API.BL.Handlers
{
    public class SetAdminPasswordHandler : IRequestHandler<SetAdminPasswordCommand, AuthResponse>
    {
        private readonly ForumDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;
        public SetAdminPasswordHandler(ForumDbContext dbContext, IPasswordHasher passwordHasher)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }

        public Task<AuthResponse> Handle(SetAdminPasswordCommand request, CancellationToken cancellationToken)
        {
            var admin = dbContext.Users.First(x => x.Email == request.Email);
            var passwordHash = passwordHasher.HashPassword(request.Password);
            admin.Password = passwordHash;
            admin.PasswordSetRequired = false;
            dbContext.SaveChanges();
            var token = JWTSecurityTokenGenerator.GetToken(admin.Username, UserRole.Admin);
            return Task.FromResult(new AuthResponse { IsSuccess = true, Message = "Authorized", JWTToken = token});
        }
    }
}
