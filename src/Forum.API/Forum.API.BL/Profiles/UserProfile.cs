using AutoMapper;
using Forum.API.DataObjects.UserObjects;
using Forum.API.DataObjects.UserObjects.UserResponses;

namespace Forum.API.BL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>();
        }
    }
}
