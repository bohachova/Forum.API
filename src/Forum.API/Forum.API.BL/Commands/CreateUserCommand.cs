using MediatR;
using Forum.API.DataObjects.Responses;

namespace Forum.API.BL.Commands
{
    public class CreateUserCommand : IRequest<AuthResponse>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; } = DateTime.Today;
    }
}
