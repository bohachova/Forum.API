using Forum.API.DataObjects.Responses;
using MediatR;

namespace Forum.API.BL.Queries
{
    public class AuthorizationQuery : IRequest<AuthResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
