using Forum.API.BL.Commands;
using Forum.API.DAL;
using Forum.API.DataObjects.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Forum.API.DataObjects.TopicObjects;

namespace Forum.API.BL.Handlers
{
    public class LikeDislikePostHandler : IRequestHandler<LikeDislikePostCommand, ReactionsResponse>
    {
        private readonly ForumDbContext dbContext;
        public LikeDislikePostHandler(ForumDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }
        public async Task<ReactionsResponse> Handle(LikeDislikePostCommand request, CancellationToken cancellationToken)
        {
            var post = await dbContext.Posts.Include( x => x.Reactions).FirstOrDefaultAsync(x => x.Id == request.PostId);
            if (request.Like && !post.Reactions.Any(x => x.AuthorId == request.UserId && x.Like))
            {
                post.Reactions.Add(new Reaction { AuthorId = request.UserId, TargetId = request.PostId, Like = true });
                var oppositeReaction = post.Reactions.FirstOrDefault(x => x.AuthorId == request.UserId && x.Dislike);
                if (oppositeReaction != null)
                    post.Reactions.Remove(oppositeReaction);
                await dbContext.SaveChangesAsync();
                return new ReactionsResponse { IsSuccess = true, Likes = post.Reactions.Where(x => x.Like).Count(), Dislikes = post.Reactions.Where(x => x.Dislike).Count() };
            }
            else if (request.Dislike && !post.Reactions.Any(x => x.AuthorId == request.UserId && x.Dislike))
            {
                post.Reactions.Add(new Reaction { AuthorId = request.UserId, TargetId = request.PostId, Dislike = true });
                var oppositeReaction = post.Reactions.FirstOrDefault(x => x.AuthorId == request.UserId && x.Like);
                if (oppositeReaction != null)
                    post.Reactions.Remove(oppositeReaction);
                await dbContext.SaveChangesAsync();
                return new ReactionsResponse { IsSuccess = true, Likes = post.Reactions.Where(x => x.Like).Count(), Dislikes = post.Reactions.Where(x => x.Dislike).Count() }; 
            }
            else if (request.Like && post.Reactions.Any(x => x.AuthorId == request.UserId && x.Like))
            {
                var likeReaction = post.Reactions.FirstOrDefault(x => x.AuthorId == request.UserId && x.Like);
                post.Reactions.Remove(likeReaction); 
                await dbContext.SaveChangesAsync();
                return new ReactionsResponse { IsSuccess = true, Likes = post.Reactions.Where(x => x.Like).Count(), Dislikes = post.Reactions.Where(x => x.Dislike).Count() };
            }
            else if(request.Dislike && post.Reactions.Any(x => x.AuthorId == request.UserId && x.Dislike))
            {
                var dislikeReaction = post.Reactions.FirstOrDefault(x => x.AuthorId == request.UserId && x.Dislike);
                post.Reactions.Remove(dislikeReaction);
                await dbContext.SaveChangesAsync();
                return new ReactionsResponse { IsSuccess = true, Likes = post.Reactions.Where(x => x.Like).Count(), Dislikes = post.Reactions.Where(x => x.Dislike).Count() };
            }
            return new ReactionsResponse { IsSuccess = false };
        }
    }
}
