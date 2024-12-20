using TaskManager.Application.UseCases.TaskUser.Common;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.UseCases.TaskUser.Update;

public class UpdateTask : IRequestHandler<UpdateTaskInput, TaskModelOutput>
{
    private readonly ITasksRepository _tasksRepository;

    public UpdateTask(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<TaskModelOutput> Handle(UpdateTaskInput input, CancellationToken cancellationToken)
    {
        var inputValidated = ValidateInput(input);
        var task = await _tasksRepository.GetById(inputValidated.TaskId);
        task.UpdateTask(input.Title, input.Description, inputValidated.Category);
        await _tasksRepository.Update(task);



        return TaskModelOutput.FromTask(task);

    }

    private UpdateTaskInput ValidateInput(UpdateTaskInput input)
    {
        if (string.IsNullOrWhiteSpace(input.Title)) input.Title = null;
        if (string.IsNullOrWhiteSpace(input.Description)) input.Description = null;
        //if (
        //       input.Category != CategoryEnuns.Personal ||
        //       input.Category != CategoryEnuns.Work ||
        //       input.Category != CategoryEnuns.Study ||
        //       input.Category != CategoryEnuns.Others ||
        //       input.Category != null)
        //{
        //    input.Category = null;
        //}
        return input;


    }
}
