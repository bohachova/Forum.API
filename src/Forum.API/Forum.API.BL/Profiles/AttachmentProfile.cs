using AutoMapper;
using Forum.API.DataObjects.TopicObjects.TopicResponses;
using Forum.API.DataObjects.TopicObjects;

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
