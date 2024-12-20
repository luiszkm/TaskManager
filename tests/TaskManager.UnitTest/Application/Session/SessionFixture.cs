using System.Security.Cryptography;
using System.Text;
using TaskManager.Domain.Authorization;
using TaskManager.Domain.Repositories;
using TaskManager.UnitTest.Common;

namespace TaskManager.UnitTest.Application.Session;

[CollectionDefinition(nameof(SessionFixtureCollection))]
public class SessionFixtureCollection : ICollectionFixture<SessionFixture>
{
}

public class SessionFixture : BaseFixture
{
    public string GetUserName() => Faker.Internet.UserName();
    public string GetPassword() => Faker.Internet.Password();

    public Mock<IAuthorization> GetAuthRepositoryMock()
    {
        var mock = new Mock<IAuthorization>();

        mock.Setup(a => a.ComputeSha256Hash(It.IsAny<string>()))
            .Returns((string password) => ComputeSha256Hash(password));

        return mock;

    }

    public Mock<IUserRepository> GetUserRepositoryMock()
   => new();
    public string ComputeSha256Hash(string password)
    {
        System.Diagnostics.Debug.WriteLine($"Computing SHA-256 hash for password: {password}");
        var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        var builder = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            builder.Append(hash[i].ToString("X2"));
        }

        return builder.ToString();
    }

    public DomainEntity.User GetValidUser(
     string username,
     string password)
     => new(username, ComputeSha256Hash(password));


}
