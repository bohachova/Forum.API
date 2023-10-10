using MediatR;
using Microsoft.AspNetCore.Mvc;
using Forum.API.DataObjects.UserObjects;
using Forum.API.BL.Queries;
using Forum.API.DataObjects.Responses;
using Forum.API.DataObjects.Enums;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Forum.API.BL.Commands;

namespace Forum.API.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator mediator;
        public AuthorizationController(IMediator mediator)
        {
              this.mediator = mediator;
        }
        [HttpPost("AuthorizationCheck")]
        public async Task<IActionResult> CheckIfPasswordSetRequired([FromBody] string email)
        {
            var query = new CheckIfPasswordSetRequiredQuery { Email = email };
            var required = await mediator.Send(query);
            if (required)
            {
                return Ok(new AuthResponse { IsSuccess = false, Message = "Password Set Required" });
            }

            return Ok(new AuthResponse { IsSuccess = true });
        }

        [HttpPost("Authorization")]
        public async Task<IActionResult> SignIn([FromBody] UserAuthorizationModel user)
        {
            if (ModelState.IsValid)
            {
                var query = new AuthorizationQuery { Email = user.Email, Password = user.Password };
                var result = await mediator.Send(query);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(new AuthResponse { IsSuccess = false, Message = "Incorrect data entered"});
        }

        [HttpPost("AdminStart")]
        public async Task<IActionResult> SetAdminPassword([FromBody] UserAuthorizationModel user)
        {
            if (ModelState.IsValid)
            {
                var command = new SetAdminPasswordCommand { Email = user.Email, Password = user.Password };
                var result = await mediator.Send(command);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
