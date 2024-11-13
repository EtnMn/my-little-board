using Etn.MyLittleBoard.Domain.Constants;
using Vogen;

namespace Etn.MyLittleBoard.Domain.Aggregates.Projects;

public sealed class Project(ProjectName name) : EntityBase<Project, ProjectId>
{
    public ProjectName Name { get; private set; } = name;
}

[ValueObject<int>]
public readonly partial struct ProjectId;

[ValueObject<string>]
public readonly partial struct ProjectName
{
    internal static Validation Validate(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Validation.Invalid($"Project {nameof(name)} cannot be empty");
        }
        else if (name.Length > ValidationConstants.DefaultNameLength)
        {
            return Validation.Invalid($"Project {nameof(name)} exceeds maximum length of {ValidationConstants.DefaultNameLength} characters");
        }
        else
        {
            return Validation.Ok;
        }
    }

    internal static string NormalizeInput(string name)
    {
        return name?.Trim() ?? string.Empty;
    }
}
