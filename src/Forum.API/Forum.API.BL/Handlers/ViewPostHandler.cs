using AutoMapper;
using Forum.API.BL.Queries;
using Forum.API.DAL;
using Forum.API.DataObjects.Pagination;
using Forum.API.DataObjects.TopicObjects.TopicResponses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.BL.Handlers
{
    public class ViewPostHandler : IRequestHandler<ViewPostQuery, PostResponse>
    {
        private readonly ForumDbContext dbContext;
        private readonly IMapper mapper;
        public ViewPostHandler(ForumDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PostResponse> Handle(ViewPostQuery request, CancellationToken cancellationToken)
        {
            var post = await dbContext.Posts.Where(x=> x.Id == request.PostId)
                                            .Include(x=>x.Author)
                                            .Include(x=>x.Attachments)
                                            .Include(x => x.Reactions)
                                            .FirstOrDefaultAsync();
            var comments = await dbContext.Comments.Where(x => x.PostId == request.PostId)
                                                   .Include(x => x.Author)
                                                   .Include(x => x.Reactions)
                                                   .Include(x => x.Parent)
                                                   .Skip((request.PageIndex - 1) * request.PageSize)
                                                   .Take(request.PageSize)
                                                   .ToListAsync();
            var postResp = mapper.Map<PostResponse>(post);
            var commentsResp = mapper.Map<List<CommentResponse>>(comments);
            foreach(var comment in commentsResp)
            {
                comment.HasChildComments = await dbContext.Comments.Where(x => x.ParentId == comment.Id).AnyAsync();
            }
            PaginatedList<CommentResponse> paginatedComments = new PaginatedList<CommentResponse> (commentsResp, comments.Count, request.PageIndex, request.PageSize);
            postResp.Comments = paginatedComments;
            return postResp;
        }
    }
}
