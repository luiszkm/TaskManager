using TaskManager.Domain.Validations;
using TaskManager.Domain.ValuObjects;

namespace TaskManager.Domain.Entities;

public class User : Entity
{
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public List<Guid> Tasks { get; private set; }
    public User(string userName, string password)
    {
        UserName = userName;
        Password = password;
        Tasks = new List<Guid>();
        ValidateUSer();
    }
    public void AddTask(Guid taskId)
    {
        Tasks.Add(taskId);
    }
    public void RemoveTask(Guid taskId)
    {
        Tasks.Remove(taskId);
    }
    public void ChangePassword(string password)
    {
        Password = password;
    }
    private void ValidateUSer()
    {
        DomainValidation.MinLength(UserName, 3, nameof(UserName));
        DomainValidation.MaxLength(UserName, 50, nameof(UserName));
        DomainValidation.NotNullOrEmpty(UserName, nameof(UserName));
        DomainValidation.MinLength(Password, 3, nameof(Password));
        DomainValidation.MaxLength(Password, 50, nameof(Password));
        DomainValidation.NotNullOrEmpty(Password, nameof(Password));
    }
}

