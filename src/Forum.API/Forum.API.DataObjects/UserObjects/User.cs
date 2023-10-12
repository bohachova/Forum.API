using System.ComponentModel.DataAnnotations;
using Forum.API.DataObjects.Enums;

namespace Forum.API.DataObjects.UserObjects
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
        public UserRole UserRole { get; set; } = UserRole.User;
        public DateTime RegistrationDate { get; set; } = DateTime.Today;
        public string? UserPhoto { get; set; }
        public bool PasswordSetRequired { get; set; } = false;
    }
}
