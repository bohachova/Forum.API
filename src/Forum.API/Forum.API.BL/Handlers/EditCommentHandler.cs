using Forum.API.BL.Commands;
using Forum.API.DAL;
using Forum.API.DataObjects.Responses;
using MediatR;

namespace Forum.API.BL.Handlers
{
    public class EditCommentHandler : IRequestHandler<EditCommentCommand, Response>
    {
        private readonly ForumDbContext dbContext;
        private readonly int minutesForEdit = 5;
        public EditCommentHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response> Handle(EditCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = dbContext.Comments.FirstOrDefault(x=> x.Id == request.Id);
            var editTimeline = comment.PublishingTime.AddMinutes(minutesForEdit);
            if(editTimeline > request.EditingTime && comment.AuthorId == request.AuthorId)
            {
                comment.Text = request.Text;
                comment.WasEdited = true;
                comment.LastEdited = request.EditingTime;
                await dbContext.SaveChangesAsync();
                return new Response { IsSuccess = true };
            }
            else
            {
                return new Response { IsSuccess = false };
            }
        }
    }
}
