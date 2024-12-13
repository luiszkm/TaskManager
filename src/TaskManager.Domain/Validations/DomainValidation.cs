
using TaskManager.Domain.Exceptions;

namespace TaskManager.Domain.Validations;

public class DomainValidation
{
    public static void NotNull(object? target, string targetName)
    {
        if (target is null) throw new EntityValidationException($"{targetName} should not be null");

    }
    public static void NotNullOrEmpty(string? target, string targetName)
    {
        if (String.IsNullOrWhiteSpace(target))
            throw new EntityValidationException($"{targetName} should not be empty or null");
    }
    public static void MinLength(string target, int minLength, string targetName)
    {
        if (target.Length < minLength) throw new EntityValidationException($"{targetName} should be at {minLength} characters long");

    }
    public static void MaxLength(string target, int maxLength, string targetName)
    {
        if (target.Length > maxLength) throw new EntityValidationException($"{targetName} should be at most {maxLength} characters long");

    }

}
