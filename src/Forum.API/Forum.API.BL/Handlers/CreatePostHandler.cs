using MediatR;
using Forum.API.BL.Commands;
using Forum.API.DataObjects.Responses;
using Forum.API.DAL;
using Forum.API.DataObjects.TopicObjects;
using Forum.API.BL.Configuration.Interfaces;
using Forum.API.BL.Services;

namespace Forum.API.BL.Handlers
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        private readonly IFileValidationConfiguration data;
        public CreatePostHandler(ForumDbContext dbContext, IFileValidationConfiguration data)
        {
            this.dbContext = dbContext;
            this.data = data;
        }
        public async Task<Response> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post { Header = request.Header, Text = request.Text, AuthorId = request.AuthorId, TopicId = request.TopicId };
            if (request.Attachments.Any())
            {
                var attachments = new List<Attachment>();
                foreach (var file in request.Attachments) 
                {
                    if(data.Extensions.Any(x=> x == Path.GetExtension(file.FileName) && file.Length < data.MaxSize))
                    {
                        var img = ImageConverter.ConvertImage(file);
                        attachments.Add(new Attachment { File = img});
                    }
                    else
                    {
                        return new Response { IsSuccess = false, Message = "File extension or file size is not valid" };
                    }
                }
                post.Attachments = attachments;
            }
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();
            return new Response { IsSuccess = true };
        }
    }
}
