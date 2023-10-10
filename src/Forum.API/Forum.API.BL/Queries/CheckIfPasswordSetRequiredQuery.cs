using MediatR;

namespace Forum.API.BL.Queries
{
    public class CheckIfPasswordSetRequiredQuery: IRequest<bool>
    {
        public string Email { get; set; } = string.Empty;
    }
}
