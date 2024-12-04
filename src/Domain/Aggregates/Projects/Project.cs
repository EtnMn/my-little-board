using Etn.MyLittleBoard.Domain.Constants;
using Etn.MyLittleBoard.Domain.Interfaces;
using Vogen;

namespace Etn.MyLittleBoard.Domain.Aggregates.Projects;

// Todo: EM: Add status, client, BU, tags
public sealed class Project(ProjectName name, ProjectDescription description) : EntityBase<Project, ProjectId>, IAggregateRoot
{
    public ProjectName Name { get; } = name;

    public ProjectDescription Description { get; } = description;

    public ProjectStart Start { get; } = ProjectStart.Unspecified;

    public ProjectEnd End { get; } = ProjectEnd.Unspecified;
}

// Remarks: use struct causes OutOfMemoryException, when using Select in EF Core projection.
[ValueObject<int>]
public sealed partial class ProjectId;

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

[ValueObject<string>]
public readonly partial struct ProjectDescription
{
    public static readonly ProjectDescription Unspecified = new(string.Empty);

    internal static Validation Validate(string description)
    {
        if (description is null)
        {
            return Validation.Invalid($"Project {nameof(description)} cannot be null");
        }
        else if (description.Length > ValidationConstants.DefaultNameLength)
        {
            return Validation.Invalid($"Project {nameof(description)} exceeds maximum length of {ValidationConstants.DefaultNameLength} characters");
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

[ValueObject<DateTimeOffset>]
public readonly partial struct ProjectStart
{
    public static readonly ProjectStart Unspecified = new(DateTimeOffset.MinValue);
}

[ValueObject<DateTimeOffset>]
public readonly partial struct ProjectEnd
{
    public static readonly ProjectEnd Unspecified = new(DateTimeOffset.MaxValue);
}
