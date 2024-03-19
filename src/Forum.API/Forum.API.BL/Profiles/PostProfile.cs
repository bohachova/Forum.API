using AutoMapper;
using Forum.API.DataObjects.TopicObjects.PostObjects;
using Forum.API.DataObjects.TopicObjects.TopicResponses;

namespace Forum.API.BL.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostResponse>()
                .ForMember(resp => resp.Comments, opt => opt.Ignore());
        }
    }
}
