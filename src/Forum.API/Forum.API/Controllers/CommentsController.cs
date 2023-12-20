using Forum.API.BL.Commands;
using Forum.API.DataObjects.TopicObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [ApiController]
    [Route("Comments")]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator mediator;
        public CommentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [Authorize]
        [HttpPost("NewComment")]
        public async Task<IActionResult> LeaveComment([FromBody] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var command = new LeaveCommentCommand { AuthorId = comment.AuthorId, ParentAuthorId = comment.ParentAuthorId, Text = comment.Text, ParentId = comment.ParentId, PostId = comment.PostId };
                var result = await mediator.Send(command);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("DeleteComment")]
        public async Task<IActionResult> DeleteComment([FromBody] int commentId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if (User.IsInRole("Admin"))
            {
                var command = new DeleteCommentCommand { CommentId = commentId, FullDeleteAllowed = true };
                var result = await mediator.Send(command);
                return Ok(result);
            }
            else
            {
                var command = new DeleteCommentCommand { CommentId = commentId, FullDeleteAllowed = false, UserId = userId };
                var result = await mediator.Send(command);
                return Ok(result);
            }
    
        }   
    }
}
