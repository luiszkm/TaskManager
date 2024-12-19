using TaskManager.Domain.Exceptions;
using TaskManager.UnitTest.Common;

namespace TaskManager.UnitTest.Domain.Entity;

[Collection(nameof(TaskUserFixtureCollection))]
public class TasksUserTests
{
    private readonly TaskUserFixture fixture;

    public TasksUserTests(TaskUserFixture fixture)
    {
        this.fixture = fixture;
    }
    // instantiate a valid TaskUser
    [Fact(DisplayName = nameof(InstantiateTaskUSer))]
    [Trait("Domain", "Task - Entity")]
    public void InstantiateTaskUSer()
    {
        var title = fixture.GetTitle();
        var userId = Guid.NewGuid();
        var description = fixture.GetDescription();
        var category = fixture.GetCategory();

        var taskUser = new DomainEntity.TaskUser(title, description, category, userId);

        taskUser.Title.Should().Be(title);
        taskUser.Description.Should().Be(description);
        taskUser.IsCompleted.Should().BeFalse();
        taskUser.Category.Should().Be(category);
        taskUser.UserId.Should().NotBeEmpty();
        taskUser.UserId.Should().Be(userId);
        taskUser.CreatedAt.Should().BeBefore(DateTime.UtcNow);
    }

    // throw exception when title is null
    [Theory(DisplayName = nameof(ThrowExceptionWhenTitleIsNull))]
    [Trait("Domain", "Task - Entity")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("12")]
    public void ThrowExceptionWhenTitleIsNull(string? invalidTitle)
    {
        var userId = Guid.NewGuid();
        var description = fixture.GetDescription();
        var category = fixture.GetCategory();

        Action act = () => new DomainEntity.TaskUser(invalidTitle, description, category, userId);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when description is null
    [Theory(DisplayName = nameof(ThrowExceptionWhenDescriptionIsNull))]
    [Trait("Domain", "Task - Entity")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("12")]
    public void ThrowExceptionWhenDescriptionIsNull(string? invalidDescription)
    {
        var userId = Guid.NewGuid();
        var title = fixture.GetTitle();
        var category = fixture.GetCategory();

        Action act = () => new DomainEntity.TaskUser(title, invalidDescription, category, userId);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when userId is empty
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserIdIsEmpty))]
    [Trait("Domain", "Task - Entity")]
    public void ThrowExceptionWhenUserIdIsEmpty()
    {
        var userId = Guid.Empty;
        var title = fixture.GetTitle();
        var description = fixture.GetDescription();
        var category = fixture.GetCategory();

        Action act = () => new DomainEntity.TaskUser(title, description, category, userId);

        act.Should().Throw<EntityValidationException>();
    }

    // Update task with all data
    [Fact(DisplayName = nameof(UpdateTaskWithAllData))]
    [Trait("Domain", "Task - Entity")]
    public void UpdateTaskWithAllData()
    {
        var taskUser = fixture.CreateValidTaskUser();

        var newTitle = fixture.GetTitle();
        var newDescription = fixture.GetDescription();
        var newCategory = fixture.GetCategory();

        taskUser.UpdateTask(newTitle, newDescription, newCategory);

        taskUser.Title.Should().Be(newTitle);
        taskUser.Description.Should().Be(newDescription);
        taskUser.Category.Should().Be(newCategory);
        taskUser.UpdatedAt.Should().BeAfter(taskUser.CreatedAt);
    }

    // update task only with title
    [Fact(DisplayName = nameof(UpdateTaskOnlyWithTitle))]
    [Trait("Domain", "Task - Entity")]
    public void UpdateTaskOnlyWithTitle()
    {
        var taskUser = fixture.CreateValidTaskUser();

        var newTitle = fixture.GetTitle();

        taskUser.UpdateTask(newTitle);

        taskUser.Title.Should().Be(newTitle);
        taskUser.Description.Should().Be(taskUser.Description);
        taskUser.Category.Should().Be(taskUser.Category);
        taskUser.UpdatedAt.Should().BeAfter(taskUser.CreatedAt);
    }

    // update task only with description
    [Fact(DisplayName = nameof(UpdateTaskOnlyWithDescription))]
    [Trait("Domain", "Task - Entity")]
    public void UpdateTaskOnlyWithDescription()
    {
        var taskUser = fixture.CreateValidTaskUser();

        var newDescription = fixture.GetDescription();

        taskUser.UpdateTask(description: newDescription);

        taskUser.Title.Should().Be(taskUser.Title);
        taskUser.Description.Should().Be(newDescription);
        taskUser.Category.Should().Be(taskUser.Category);
        taskUser.UpdatedAt.Should().BeAfter(taskUser.CreatedAt);
    }

    // update task only with category
    [Fact(DisplayName = nameof(UpdateTaskOnlyWithCategory))]
    [Trait("Domain", "Task - Entity")]
    public void UpdateTaskOnlyWithCategory()
    {
        var taskUser = fixture.CreateValidTaskUser();

        var newCategory = fixture.GetCategory();

        taskUser.UpdateTask(category: newCategory);

        taskUser.Title.Should().Be(taskUser.Title);
        taskUser.Description.Should().Be(taskUser.Description);
        taskUser.Category.Should().Be(newCategory);
        taskUser.UpdatedAt.Should().BeAfter(taskUser.CreatedAt);
    }

    // throw when update task with invalid title
    [Theory(DisplayName = nameof(ThrowWhenUpdateTaskWithInvalidTitle))]
    [Trait("Domain", "Task - Entity")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("12")]
    public void ThrowWhenUpdateTaskWithInvalidTitle(string? invalidTitle)
    {
        var taskUser = fixture.CreateValidTaskUser();

        Action act = () => taskUser.UpdateTask(invalidTitle);

        act.Should().Throw<EntityValidationException>();
    }

    // throw when update task with invalid description
    [Theory(DisplayName = nameof(ThrowWhenUpdateTaskWithInvalidDescription))]
    [Trait("Domain", "Task - Entity")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("12")]
    public void ThrowWhenUpdateTaskWithInvalidDescription(string? invalidDescription)
    {
        var taskUser = fixture.CreateValidTaskUser();

        Action act = () => taskUser.UpdateTask(description: invalidDescription);

        act.Should().Throw<EntityValidationException>();
    }

    // mark task as completed
    [Fact(DisplayName = nameof(MarkTaskAsCompleted))]
    [Trait("Domain", "Task - Entity")]
    public void MarkTaskAsCompleted()
    {
        var taskUser = fixture.CreateValidTaskUser();

        taskUser.MarkAsCompleted();

        taskUser.IsCompleted.Should().BeTrue();
        taskUser.UpdatedAt.Should().BeBefore(DateTime.UtcNow.AddMinutes(1));
    }
}
