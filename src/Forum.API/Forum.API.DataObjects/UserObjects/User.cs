using System.ComponentModel.DataAnnotations;
using Forum.API.DataObjects.Enums;
using Forum.API.DataObjects.TopicObjects;

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
        public string Password { get; set; } = string.Empty;
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
        public List<Topic> CreatedTopics { get; set; } = new List<Topic>();
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public bool DeletedUser { get; set; } = false;  
    }
}
