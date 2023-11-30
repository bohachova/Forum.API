using MediatR;
using Forum.API.BL.Commands;
using Forum.API.DataObjects.Responses;
using Forum.API.DAL;
using Forum.API.BL.Queries;
using Forum.API.BL.Services;
using Forum.API.BL.Configuration.Interfaces;

namespace Forum.API.BL.Handlers
{
    public class SetUserPhotoHandler : IRequestHandler<SetUserPhotoCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        private readonly IMediator mediator;
        private readonly IFileValidationConfiguration data;
        public SetUserPhotoHandler(ForumDbContext dbContext, IMediator mediator, IFileValidationConfiguration data)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            this.data = data;
        }
        public async Task<Response> Handle(SetUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var query = new FindUserQuery { Id = request.UserId};
            var user = await mediator.Send(query);
            if (data.Extensions.Any(x=> x== Path.GetExtension(request.File.FileName) && request.File.Length < data.MaxSize))
            {
                var image = ImageConverter.ConvertImage(request.File);
                user.UserPhoto = image;
                await dbContext.SaveChangesAsync();
                return new Response { IsSuccess = true };
            }
            else
            {
                return new Response { IsSuccess = false, Message = "File extension or file size is not valid" };
            }
        }
    }
}
