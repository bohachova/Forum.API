using AutoMapper;
using Forum.API.DataObjects.TopicObjects;
using Forum.API.DataObjects.TopicObjects.TopicResponses;

namespace Forum.API.BL.Profiles
{
    public class ReactionProfile : Profile
    {
        public ReactionProfile()
        {
            CreateMap<Reaction, ReactionResponse>();
        }
    }
}
