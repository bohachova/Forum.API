using MediatR;
using Forum.API.BL.Queries;
using Forum.API.DAL;
using Forum.API.DataObjects.Enums;

namespace Forum.API.BL.Handlers
{
    public class CheckIfPasswordSetRequiredHandler : IRequestHandler<CheckIfPasswordSetRequiredQuery, bool>
    {
        private readonly ForumDbContext dbContext;
        public CheckIfPasswordSetRequiredHandler(ForumDbContext dbContext)
        {
             this.dbContext = dbContext;
        }
        public Task<bool> Handle(CheckIfPasswordSetRequiredQuery request, CancellationToken cancellationToken)
        {
            var user = dbContext.Users.FirstOrDefault(x=>x.Email == request.Email);
            if (user != null && user.UserRole == UserRole.Admin && user.PasswordSetRequired == true)
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }
    }
}
