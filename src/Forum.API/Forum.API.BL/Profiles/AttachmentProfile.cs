using AutoMapper;
using Forum.API.DataObjects.TopicObjects;
using Forum.API.DataObjects.TopicObjects.TopicResponses;

namespace Forum.API.BL.Profiles
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<Attachment, AttachmentResponse>();
        }
    }
}
