namespace TaskManager.Application.UseCases.TaskUser.Delete;

public class DeleteTaskInput : IRequest
{
    public Guid Id { get; set; }

    public DeleteTaskInput(Guid id)
    {
        Id = id;
    }
}
