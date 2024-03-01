using Forum.API.BL.Commands;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.Pagination;
using Forum.API.DataObjects.Responses;
using Forum.API.DataObjects.TopicObjects;
using Forum.API.DataObjects.TopicObjects.PostObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [ApiController]
    [Route("Topics")]
    public class TopicsController : ControllerBase
    {
        private readonly IMediator mediator;
        public TopicsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("AllTopics")]
        public async Task<IActionResult> GetTopicsList([FromBody] PaginationSettings settings)
        {
            var query = new GetTopicsListQuery { PageIndex = settings.PageNumber, PageSize = settings.PageSize };
            var result = await mediator.Send(query);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("Posts/{topicId}")]
        public async Task<IActionResult> GetPosts([FromRoute]int topicId, [FromBody] PaginationSettings settings)
        {
            var query = new GetTopicPostsQuery { PageIndex = settings.PageNumber, PageSize = settings.PageSize, TopicId = topicId };
            var result = await mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("NewTopic")]
        public async Task<IActionResult> CreateTopic([FromBody] Topic topic)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateTopicCommand { Name = topic.Title, AuthorId = topic.AuthorId };
                var result = await mediator.Send(command);
                return Ok(result);
            }
            else
            {
                return BadRequest(new Response { IsSuccess = false });
            }
        }
        [Authorize]
        [HttpPost("NewPost")]
        public async Task<IActionResult> CreatePost([FromForm] PostCreationModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreatePostCommand
                {
                    Header = model.Header,
                    Text = model.Text,
                    AuthorId = model.AuthorId,
                    TopicId = model.TopicId,
                    Attachments = model.Attachments
                };
                var result = await mediator.Send(command);
                return Ok(result);
            }
            else
            {
                return BadRequest(new Response { IsSuccess = false, Message = "Not valid data"});
            }
        }
        [Authorize]
        [HttpPost("DeletePost")]
        public async Task<IActionResult> DeletePost([FromBody] int postId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if(User.IsInRole("Admin"))
            {
                var command = new DeletePostCommand { PostId = postId, FullDeleteAllowed = true};
                var result = await mediator.Send(command);
                return Ok(result);
            }
            else
            {
                var command = new DeletePostCommand { PostId = postId, FullDeleteAllowed = false, UserId = userId };
                var result = await mediator.Send(command);
                return Ok(result);
            }
        }
        [Authorize]
        [HttpPost("ViewPost/{postId}")]
        public async Task<IActionResult> ViewPost([FromRoute] int postId, [FromBody] PaginationSettings settings)
        {
            var query = new ViewPostQuery { PostId = postId, PageIndex = settings.PageNumber, PageSize = settings.PageSize };
            var result = await mediator.Send(query);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("EditPost")]
        public async Task<IActionResult> EditPost([FromForm]PostEditModel model)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if (ModelState.IsValid)
            {
                string[] deletedAttArray = model.DeletedAttachmentsString.Split(",");
                var deletedAttList = new List<int>();
                if (!deletedAttArray.Contains("0"))
                {
                    foreach (var item in deletedAttArray)
                    {
                        deletedAttList.Add(int.Parse(item));
                    }
                }
                var command = new EditPostCommand
                {
                    Id = model.Id,
                    Header = model.Header,
                    Text = model.Text,
                    NewAttachments = model.NewAttachments,
                    DeletedAttachments = deletedAttList,
                    UserId = userId
                };
                var result = await mediator.Send(command);
                return Ok(result);
            }
            return BadRequest(new Response { IsSuccess = false, Message = "Not valid data" });
        }
        [Authorize]
        [HttpPost("PostReaction")]
        public async Task<IActionResult> LikeDislikePost([FromBody] LikeDislikeReactionRequest reaction)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var command = new LikeDislikePostCommand 
            { 
                UserId = userId, 
                PostId = reaction.TargetId,
                Like = reaction.Like, 
                Dislike = reaction.Dislike  
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }

    }
}
