using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.APIModel;
using TaskManager.Application.UseCases.TaskUser.ChangeStatus;
using TaskManager.Application.UseCases.TaskUser.Common;
using TaskManager.Application.UseCases.TaskUser.Create;
using TaskManager.Application.UseCases.TaskUser.Delete;
using TaskManager.Application.UseCases.TaskUser.Get;
using TaskManager.Application.UseCases.TaskUser.ListTasks;
using TaskManager.Application.UseCases.TaskUser.Update;

namespace TaskManager.API.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<TaskModelOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult>
        Create([FromBody] CreateTaskInput inputModel)
    {
        var output = await _mediator.Send(inputModel);
        var response = new ApiResponse<TaskModelOutput>(output);
        return CreatedAtAction(
                       nameof(Create),
                                  new { id = output.Id }, response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<TaskModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var output = await _mediator.Send(new GetTaskInput(id));
        var response = new ApiResponse<TaskModelOutput>(output);

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<TaskModelOutput>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteTaskInput(id));
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<TaskModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskInput inputModel)
    {
        inputModel.TaskId = id;
        var output = await _mediator.Send(inputModel);
        var response = new ApiResponse<TaskModelOutput>(output);
        return Ok(response);
    }

    [HttpPatch("changestatus")]
    [ProducesResponseType(typeof(ApiResponse<TaskModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<IActionResult>
    FinishedProject([FromBody] ChangeStatusInput inputModel)
    {
        var output = await _mediator.Send(inputModel);
        var response = new ApiResponse<TaskModelOutput>(output);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<TaskModelOutput>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Filter([FromQuery] ListTasksInput inputModel)
    {
        var output = await _mediator.Send(inputModel);
        var response = new ApiResponse<List<TaskModelOutput>>(output);
        return Ok(response);
    }

}


