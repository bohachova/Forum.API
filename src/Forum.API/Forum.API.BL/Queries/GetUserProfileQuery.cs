using Forum.API.DataObjects.UserObjects.UserResponses;
using MediatR;

namespace Forum.API.BL.Queries
{
    public class GetUserProfileQuery : IRequest<UserResponse>
    {
        public int Id { get; set; }
    }
}
