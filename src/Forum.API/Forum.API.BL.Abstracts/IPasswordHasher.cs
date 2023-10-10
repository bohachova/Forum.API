

namespace Forum.API.BL.Abstracts
{
    public interface IPasswordHasher
    {
        string HashPassword (string password);
        bool VerifyPassword (string password, string savedHash);
    }
}
