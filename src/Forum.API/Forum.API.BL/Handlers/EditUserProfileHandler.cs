using MediatR;
using Forum.API.BL.Commands;
using Forum.API.DAL;
using Forum.API.BL.Queries;

namespace Forum.API.BL.Handlers
{
    public class EditUserProfileHandler : IRequestHandler<EditUserProfileCommand, Unit>
    {
        private readonly ForumDbContext dbContext;
        private readonly IMediator mediator;
        public EditUserProfileHandler(ForumDbContext dbContext, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
        }
        public async Task<Unit> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
        {
            var query = new FindUserQuery { Id = request.Id };
            var user = await mediator.Send(query);
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Country = request.Country;
            user.City = request.City;
            user.About = request.About;
            if (user.DateOfBirth != request.DateOfBirth &&
                request.DateOfBirth > DateTime.Now.AddYears(-90) &&
                request.DateOfBirth < DateTime.Now.AddYears(-10))
            {
                user.DateOfBirth = request.DateOfBirth;
            }
            if (user.Username != request.Username)
            {
                var queryUniqueUsername = new FindUserQuery { Username = request.Username };
                var userWithUsername = await mediator.Send(queryUniqueUsername);
                if (userWithUsername == null)
                {
                    user.Username = request.Username;
                }
            }
            dbContext.SaveChanges();
            return Unit.Value;
        }
    }
}