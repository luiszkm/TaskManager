using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.APIModel;
using TaskManager.Application.UseCases.User.Common;
using TaskManager.Application.UseCases.User.Create;
using TaskManager.Application.UseCases.User.GetById;
using TaskManager.Application.UseCases.User.ListUsers;

namespace TaskManager.API.Controllers;
[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    => _mediator = mediator;




    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<UserModelOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult>
        Create([FromBody] CreateUserInput inputModel)
    {


        var output = await _mediator.Send(inputModel);
        var response = new ApiResponse<UserModelOutput>(output);
        return CreatedAtAction(
            nameof(Create),
            new { id = output.Id }, response);


    }


    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<UserModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var output = await _mediator.Send(new GetUserByIdInput(id));
        var response = new ApiResponse<UserModelOutput>(output);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<UserModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Get()
    {
        var output = await _mediator.Send(new ListUsersInput());
        var response = new ApiResponse<List<UserModelOutput>>(output);
        return Ok(response);
    }


}
