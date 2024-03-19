using Forum.API.BL.Commands;
using Forum.API.DAL;
using Forum.API.DataObjects.Responses;
using Forum.API.DataObjects.TopicObjects;
using Forum.API.DataObjects.TopicObjects.PostObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class LikeDislikeCommentHandler : IRequestHandler<LikeDislikeCommentCommand, ReactionsResponse>
    {
        private readonly ForumDbContext dbContext;
        public LikeDislikeCommentHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ReactionsResponse> Handle(LikeDislikeCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await dbContext.Comments.Include(x => x.Reactions).FirstOrDefaultAsync(x => x.Id == request.CommentId);

            if (request.Like && !comment.Reactions.Any(x => x.AuthorId == request.UserId && x.Like))
            {
                comment.Reactions.Add(new Reaction { AuthorId = request.UserId, TargetId = request.CommentId, Like = true });
                var oppositeReaction = comment.Reactions.FirstOrDefault(x => x.AuthorId == request.UserId && x.Dislike);
                if (oppositeReaction != null)
                    comment.Reactions.Remove(oppositeReaction);
                await dbContext.SaveChangesAsync();
                return new ReactionsResponse { IsSuccess = true, Likes = comment.Reactions.Where(x => x.Like).Count(), Dislikes = comment.Reactions.Where(x => x.Dislike).Count() };

            }
            else if (request.Dislike && !comment.Reactions.Any(x => x.AuthorId == request.UserId && x.Dislike))
            {
                comment.Reactions.Add(new Reaction { AuthorId = request.UserId, TargetId = request.CommentId, Dislike = true });
                var oppositeReaction = comment.Reactions.FirstOrDefault(x => x.AuthorId == request.UserId && x.Like);
                if (oppositeReaction != null)
                    comment.Reactions.Remove(oppositeReaction);
                await dbContext.SaveChangesAsync();
                return new ReactionsResponse { IsSuccess = true, Likes = comment.Reactions.Where(x => x.Like).Count(), Dislikes = comment.Reactions.Where(x => x.Dislike).Count() };
            }
            else if (request.Like && comment.Reactions.Any(x => x.AuthorId == request.UserId && x.Like))
            {
                var likeReaction = comment.Reactions.FirstOrDefault(x => x.AuthorId == request.UserId && x.Like);
                comment.Reactions.Remove(likeReaction);
                await dbContext.SaveChangesAsync();
                return new ReactionsResponse { IsSuccess = true, Likes = comment.Reactions.Where(x => x.Like).Count(), Dislikes = comment.Reactions.Where(x => x.Dislike).Count() };
            }
            else if (request.Dislike && comment.Reactions.Any(x => x.AuthorId == request.UserId && x.Dislike))
            {
                var dislikeReaction = comment.Reactions.FirstOrDefault(x => x.AuthorId == request.UserId && x.Dislike);
                comment.Reactions.Remove(dislikeReaction);
                await dbContext.SaveChangesAsync();
                return new ReactionsResponse { IsSuccess = true, Likes = comment.Reactions.Where(x => x.Like).Count(), Dislikes = comment.Reactions.Where(x => x.Dislike).Count() };
            }
            return new ReactionsResponse { IsSuccess = false };
        }
    }
}
