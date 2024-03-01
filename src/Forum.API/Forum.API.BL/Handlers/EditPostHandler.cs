using Forum.API.BL.Commands;
using Forum.API.BL.Configuration.Interfaces;
using Forum.API.BL.Services;
using Forum.API.DAL;
using Forum.API.DataObjects.Responses;
using Forum.API.DataObjects.TopicObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class EditPostHandler : IRequestHandler<EditPostCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        private readonly IFileValidationConfiguration data;
        public EditPostHandler(ForumDbContext dbContext, IFileValidationConfiguration data)
        {
            this.dbContext = dbContext;
            this.data = data;
        }
        public async Task<Response> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            var post = await dbContext.Posts.Include(x=> x.Attachments).FirstOrDefaultAsync(x=>x.Id == request.Id);
            if(post.AuthorId == request.UserId)
            {
                post.Header = request.Header;
                post.Text = request.Text;
                post.WasEdited = true;
                post.LastEdited = request.EditTime;

                if (!post.Attachments.Any() && request.NewAttachments.Any())
                {
                    var attachments = new List<Attachment>(5);
                    foreach (var file in request.NewAttachments)
                    {
                        if (data.Extensions.Any(x => x == Path.GetExtension(file.FileName) && file.Length < data.MaxSize))
                        {
                            var img = ImageConverter.ConvertImage(file);
                            attachments.Add(new Attachment { File = img });
                        }
                    }
                    post.Attachments = attachments;
                }
                else if (post.Attachments.Any())
                {
                    if (request.DeletedAttachments.Any())
                    {
                        foreach (var id in request.DeletedAttachments)
                        {
                            var item = post.Attachments.FirstOrDefault(x => x.Id == id);
                            post.Attachments.Remove(item);
                        }
                    }
                    if (request.NewAttachments.Any())
                    {
                        while (post.Attachments.Count() < 5)
                        {
                            foreach (var file in request.NewAttachments)
                            {
                                if (data.Extensions.Any(x => x == Path.GetExtension(file.FileName) && file.Length < data.MaxSize))
                                {
                                    var img = ImageConverter.ConvertImage(file);
                                    post.Attachments.Add(new Attachment { File = img });
                                }
                            }
                        }
                    }
                }
                await dbContext.SaveChangesAsync();
                return new Response { IsSuccess = true };
            }
            return new Response { IsSuccess = false };
        }
    }
}
