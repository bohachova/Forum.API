using MediatR;
using Forum.API.BL.Queries;
using Forum.API.DAL;
using Forum.API.DataObjects.Enums;

namespace Forum.API.BL.Handlers
{
    public class CheckIfPasswordSetRequiredHandler : IRequestHandler<CheckIfPasswordSetRequiredQuery, bool>
    {
        private readonly IMediator mediator;
        public CheckIfPasswordSetRequiredHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<bool> Handle(CheckIfPasswordSetRequiredQuery request, CancellationToken cancellationToken)
        {
            var query = new FindUserQuery { Email = request.Email };
            var user = await mediator.Send(query);
            if (user != null && user.UserRole == UserRole.Admin && user.PasswordSetRequired == true)
                return true;
            else
                return false;
        }
    }
}
