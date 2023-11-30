using Forum.API.DataObjects.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.API.BL.Commands
{
    public class EditUserProfileCommand: IRequest<Unit>
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string About { get; set; } = string.Empty;
    }
}
