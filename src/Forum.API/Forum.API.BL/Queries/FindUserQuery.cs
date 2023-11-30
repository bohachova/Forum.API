using MediatR;
using Forum.API.DataObjects.UserObjects;

namespace Forum.API.BL.Queries
{
    public class FindUserQuery: IRequest<User?>
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
