namespace TaskManager.Domain.Authorization;

public interface IAuthorization
{
    string GenerateToken(Guid userId);
    string ComputeSha256Hash(string password);
}
