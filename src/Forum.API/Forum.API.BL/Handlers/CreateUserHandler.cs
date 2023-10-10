using MediatR;
using Forum.API.BL.Commands;
using Forum.API.DataObjects.Responses;
using Forum.API.DAL;
using Forum.API.BL.Abstracts;
using Forum.API.DataObjects.UserObjects;
using Forum.API.BL.Security;
using Forum.API.DataObjects.Enums;

namespace Forum.API.BL.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, AuthResponse>
    {
        private readonly ForumDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;
        public CreateUserHandler(ForumDbContext dbContext, IPasswordHasher passwordHasher)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }
        public Task<AuthResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (dbContext.Users.Any(x => x.Email == request.Email || x.Username == request.Username)) 
            {
                var result = new AuthResponse { IsSuccess = false, Message = "Email or username is already in use"};
                return Task.FromResult(result);
            }
            else
            {
                var password = passwordHasher.HashPassword(request.Password);
                dbContext.Users.Add(new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    Password = password
                });
                dbContext.SaveChanges();
                var token = JWTSecurityTokenGenerator.GetToken(request.Username, UserRole.User);
                var result = new AuthResponse { IsSuccess = true, Message = "Registered", JWTToken = token };
                return Task.FromResult(result);
            }
        }
    }
}
