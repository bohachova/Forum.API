using Microsoft.AspNetCore.Mvc;
using MediatR;
using Forum.API.DataObjects.UserObjects;
using Forum.API.BL.Commands;
using Forum.API.DataObjects.Responses;

namespace Forum.API.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class RegistrationController : ControllerBase
    {
        private readonly IMediator mediator;
        public RegistrationController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> SignUp ([FromBody]UserRegistrationModel user)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateUserCommand { Username  = user.Username, Email = user.Email, Password = user.Password };
                var result = await mediator.Send(command);
                return Ok(result);
            }
            return BadRequest(new AuthResponse { IsSuccess = false, Message = "Incorrect data entered" });
        }
    }
}
