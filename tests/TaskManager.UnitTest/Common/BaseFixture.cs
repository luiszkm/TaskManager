namespace TaskManager.UnitTest.Common;


public class BaseFixture
{
    public Faker Faker { get; private set; }
    public BaseFixture() => Faker = new Faker();

}
