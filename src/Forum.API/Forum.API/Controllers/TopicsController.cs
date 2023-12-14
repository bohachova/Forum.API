using Forum.API.BL.Commands;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.Pagination;
using Forum.API.DataObjects.Responses;
using Forum.API.DataObjects.TopicObjects;
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
                return BadRequest(new Response { IsSuccess = false});
            }
        }
    }
}
