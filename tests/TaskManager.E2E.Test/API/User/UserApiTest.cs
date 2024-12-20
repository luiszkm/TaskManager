using System.Net;
using TaskManager.API.APIModel;
using TaskManager.Application.UseCases.Session;
using TaskManager.Application.UseCases.User.Common;
using TaskManager.Application.UseCases.User.GetById;
using TaskManager.E2E.Test.API.User.Common;

namespace TaskManager.E2E.Test.API.User;

[Collection(nameof(UserAPITestFixtureCollection))]
public class UserApiTest
{
    private readonly UserAPITestFixture _fixture;

    public UserApiTest(UserAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateUserTest))]
    [Trait("E2E/API", "User/Create - Endpoints")]
    public async Task CreateUserTest()
    {
        // Arrange
        var input = _fixture.CreateValidUserInput();
        // Act
        var (response, output) = await _fixture.ApiClient.Post<ApiResponse<UserModelOutput>>
            ("/user", input);

        response!.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Should().NotBeNull();

        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
    }

    [Fact(DisplayName = nameof(GetUserByIdTest))]
    [Trait("E2E/API", "User/GetById - Endpoints")]
    public async Task GetUserByIdTest()
    {
        // Arrange
        var user = _fixture.GeteValidUser();
        var dbContext = _fixture.Persistence.InsertUser(user);


        // Act
        var (response, output) = await _fixture.ApiClient.Get<ApiResponse<UserModelOutput>>
            ($"/user/{user.Id}");

        // Assert
        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();

        output!.Data.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(GetUserByIdNotFoundTest))]
    [Trait("E2E/API", "User/GetById - Endpoints")]
    public async Task GetUserByIdNotFoundTest()
    {
        // Arrange
        var input = new GetUserByIdInput(Guid.NewGuid());
        // Act
        var (response, output) = await _fixture.ApiClient.Get<ApiResponse<UserModelOutput>>
            ($"/user/{input.Id}", input);

        // Assert
        response!.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Should().NotBeNull();

        output!.Data.Should().BeNull();
    }

    // get all users
    [Fact(DisplayName = nameof(GetAllUsersTest))]
    [Trait("E2E/API", "User/GetAll - Endpoints")]
    public async Task GetAllUsersTest()
    {
        // Arrange
        var users = _fixture.GeteValidUser();
        var dbContext = _fixture.GetDbContextInMemory();
        await dbContext.Users.AddAsync(users);
        await dbContext.SaveChangesAsync();
        // Act
        var (response, output) = await _fixture.ApiClient.Get<ApiResponse<List<UserModelOutput>>>
            ("/user");

        // Assert
        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();

        output!.Data.Should().NotBeNull();
        output!.Data!.Should().HaveCount(1);
    }

    // login
    [Fact(DisplayName = nameof(LoginUserTest))]
    [Trait("E2E/API", "User/Login - Endpoints")]

    public async Task LoginUserTest()
    {
        var password = _fixture.GetPassword();
        var dbContext = _fixture.GetDbContextInMemory(true);
        var user = _fixture.CreateValidUserWithoutPassword(password);
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var inputModel = new SessionInput(
            user.UserName,
            password
        );
        var (response, output) = await _fixture
        .ApiClient.Post<ApiResponse
                           <SessionOutPut>>("/session", inputModel);


        response!.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Should().NotBeNull();

        output!.Data.Should().NotBeNull();
        output!.Data.UserId.Should().Be(user.Id);
        output!.Data.Token.Should().NotBeEmpty();

    }

}
