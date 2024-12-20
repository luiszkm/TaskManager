using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.APIModel;
using TaskManager.Application.UseCases.Session;

namespace TaskManager.API.Controllers;
[Route("[controller]")]
[ApiController]
public class SessionController : ControllerBase
{
    private readonly IMediator _mediator;

    public SessionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public async Task<IActionResult> Create([FromBody] SessionInput inputModel)
    {

        var output = await _mediator.Send(inputModel);
        var response = new ApiResponse<SessionOutPut>(output);

        Response.Cookies.Append("authToken", output.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict
        });
        return CreatedAtAction(
            nameof(Create),
            new { id = output.UserId }, response);
    }
}
