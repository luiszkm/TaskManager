namespace TaskManager.Domain.SeedWork;

public interface IFilterRepository<T> where T : Entity
{
    Task<List<T>> Filter(
       FilterInput? filterInput);

}
