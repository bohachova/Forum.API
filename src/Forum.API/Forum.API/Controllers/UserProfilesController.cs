using MediatR;
using Microsoft.AspNetCore.Mvc;
using Forum.API.DataObjects.UserObjects;
using Forum.API.BL.Queries;
using Forum.API.BL.Commands;
using Microsoft.AspNetCore.Authorization;
using Forum.API.DataObjects.Pagination;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;
using Forum.API.DataObjects.Enums;

namespace Forum.API.Controllers
{
    [ApiController]
    [Route("Profiles")]
    public class UserProfilesController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserProfilesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AllUsers")]
        public async Task<IActionResult> GetAllProfiles([FromBody] PaginationSettings settings)
        {
            var query = new GetUserListQuery { PageIndex = settings.PageNumber, PageSize = settings.PageSize };
            var result = await mediator.Send(query);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("UserProfile/{userId:int}")]
        public async Task<IActionResult> GetUserProfile([FromRoute] int userId)
        {
            var query = new GetUserProfileQuery { Id =  userId };
            var user = await mediator.Send(query);
            return Ok(user);
        }
        [Authorize]
        [HttpPost("EditProfile")]
        public async Task<IActionResult> EditUserProfile([FromBody] User model)
        {
            if (ModelState.IsValid)
            {
                var command = new EditUserProfileCommand
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Username = model.Username,
                    Country = model.Country,
                    City = model.City,
                    About = model.About,
                    DateOfBirth = model.DateOfBirth
                };
                await mediator.Send(command);
                var query = new FindUserQuery { Id=model.Id };
                var updatedUser = await mediator.Send(query);
                return Ok(updatedUser);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpPost("SetUserPhoto/{userId}")]
        public async Task<IActionResult> ChangeUserPhoto([FromRoute] int userId, [FromForm]IFormFile file)
        {
            var command = new SetUserPhotoCommand { UserId = userId, File = file };
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("DeleteUserPhoto/{userId}")]
        public async Task<IActionResult> DeleteUserPhoto([FromRoute] int userId)
        {
            var command = new DeleteUserPhotoCommand { UserId = userId};
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("DeleteUser/{deletedUserId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int deletedUserId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var userRole = Enum.Parse<UserRole>(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
            var command = new DeleteUserCommand { UserId = userId, UserRole = userRole, DeletedUserId = deletedUserId };
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
