using AutoMapper;
using Forum.API.DataObjects.TopicObjects;
using Forum.API.DataObjects.TopicObjects.TopicResponses;

namespace Forum.API.BL.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentResponse>();
        }
    }
}
