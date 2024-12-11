using Etn.MyLittleBoard.Domain.Constants;
using Etn.MyLittleBoard.Domain.Interfaces;
using System.Text.RegularExpressions;
using Vogen;

namespace Etn.MyLittleBoard.Domain.Aggregates.Projects;

// Todo: EM: client, BU, tags
public sealed class Project(
    ProjectName name,
    ProjectDescription description,
    ProjectColor color,
    ProjectStatus projectStatus) :
    EntityBase<Project, ProjectId>,
    IAggregateRoot
{
    public Project(ProjectName name, ProjectDescription description) :
        this(name, description, ProjectColor.Unspecified, ProjectStatus.Draft)
    {
    }

    public ProjectName Name { get; } = name;

    public ProjectColor Color { get; } = color;

    public ProjectDescription Description { get; } = description;

    public ProjectStart Start { get; } = ProjectStart.Unspecified;

    public ProjectEnd End { get; } = ProjectEnd.Unspecified;

    public ProjectStatus Status { get; } = projectStatus;
}

// Remarks: use struct causes OutOfMemoryException, when using Select in EF Core projection.
[ValueObject<int>]
public sealed partial class ProjectId;

[ValueObject<string>]
public readonly partial struct ProjectName
{
    internal static Validation Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Validation.Invalid($"Project {nameof(value)} cannot be empty");
        }
        else if (value.Length > ValidationConstants.DefaultTextLength)
        {
            return Validation.Invalid($"Project {nameof(value)} exceeds maximum length of {ValidationConstants.DefaultTextLength} characters");
        }
        else
        {
            return Validation.Ok;
        }
    }

    internal static string NormalizeInput(string value)
    {
        return value?.Trim() ?? string.Empty;
    }
}

[ValueObject<string>]
public readonly partial struct ProjectColor
{
    public static readonly ProjectColor Unspecified = new(string.Empty);

    [GeneratedRegex(@"^#([A-Fa-f0-9]{6})$")]
    private static partial Regex HexColorRegex();

    internal static Validation Validate(string value)
    {
        if (value is null)
        {
            return Validation.Invalid($"Project {nameof(value)} cannot be null");
        }
        else if (!string.IsNullOrEmpty(value) && !HexColorRegex().IsMatch(value))
        {
            return Validation.Invalid($"Project {nameof(value)} is not a valid hex color");
        }
        else
        {
            return Validation.Ok;
        }
    }

    internal static string NormalizeInput(string value)
    {
        return value?.Trim() ?? string.Empty;
    }
}

[ValueObject<string>]
public readonly partial struct ProjectDescription
{
    public static readonly ProjectDescription Unspecified = new(string.Empty);

    internal static Validation Validate(string value)
    {
        if (value is null)
        {
            return Validation.Invalid($"Project {nameof(value)} cannot be null");
        }
        else if (value.Length > ValidationConstants.DefaultTextLength)
        {
            return Validation.Invalid($"Project {nameof(value)} exceeds maximum length of {ValidationConstants.DefaultTextLength} characters");
        }
        else
        {
            return Validation.Ok;
        }
    }

    internal static string NormalizeInput(string value)
    {
        return value?.Trim() ?? string.Empty;
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
