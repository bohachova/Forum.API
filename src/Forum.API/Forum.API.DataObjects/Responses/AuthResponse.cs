

namespace Forum.API.DataObjects.Responses
{
    public class AuthResponse : Response
    {
        public string JWTToken { get; set; } = string.Empty;
    }
}
