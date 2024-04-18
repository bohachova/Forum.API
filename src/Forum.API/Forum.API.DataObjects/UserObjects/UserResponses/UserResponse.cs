using Forum.API.DataObjects.Enums;

namespace Forum.API.DataObjects.UserObjects.UserResponses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole UserRole { get; set; } = UserRole.User;
        public DateTime RegistrationDate { get; set; } = DateTime.Today;
        public string? UserPhoto { get; set; }
        public bool PasswordSetRequired { get; set; } = false;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? About { get; set; }
        public bool DeletedUser { get; set; } = false;
        public bool BannedUser { get; set; } = false;
        public BanType BanType { get; set; } = BanType.NotBanned;
        public DateTime? BanTime { get; set; }
    }
}
