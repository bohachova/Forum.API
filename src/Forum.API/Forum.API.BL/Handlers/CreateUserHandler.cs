using MediatR;
using Forum.API.BL.Commands;
using Forum.API.DataObjects.Responses;
using Forum.API.DAL;
using Forum.API.BL.Abstracts;
using Forum.API.DataObjects.UserObjects;
using Forum.API.BL.Security;
using Forum.API.DataObjects.Enums;
using Forum.API.BL.Queries;

namespace Forum.API.BL.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, AuthResponse>
    {
        private readonly ForumDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMediator mediator;
        public CreateUserHandler(ForumDbContext dbContext, IPasswordHasher passwordHasher, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.mediator = mediator;
        }
        public async Task<AuthResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var query = new FindUserQuery { Email = request.Email , Username = request.Username};
            var user = await mediator.Send(query);
            if (user != null) 
            {
                var result = new AuthResponse { IsSuccess = false, Message = "Email or username is already in use"};
                return result;
            }
            else
            {
                var password = passwordHasher.HashPassword(request.Password);
                var newUser = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    Password = password
                };
                await dbContext.Users.AddAsync(newUser);
                await dbContext.SaveChangesAsync();
                var token = JWTSecurityTokenGenerator.GetToken(newUser.Id, request.Username, UserRole.User);
                var result = new AuthResponse { IsSuccess = true, Message = "Registered", JWTToken = token };
                return result;
            }
        }
    }
}