using Etn.MyLittleBoard.Domain.Constants;
using Etn.MyLittleBoard.Domain.Interfaces;
using System.Text.RegularExpressions;
using Vogen;

namespace Etn.MyLittleBoard.Domain.Aggregates.Projects;

// Todo: EM: client, tags
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

    public ProjectName Name { get; private set; } = name;

    public ProjectColor Color { get; private set; } = color;

    public ProjectDescription Description { get; private set; } = description;

    public ProjectStart Start { get; private set; } = ProjectStart.Unspecified;

    public ProjectEnd End { get; private set; } = ProjectEnd.Unspecified;

    public ProjectStatus Status { get; private set; } = projectStatus;

    public void UpdateColor(ProjectColor projectColor)
    {
        this.Color = projectColor;
    }

    public void UpdateDescription(ProjectDescription value)
    {
        this.Description = value;
    }

    public void UpdateName(ProjectName value)
    {
        this.Name = value;
    }

    public void UpdatePeriod(ProjectStart start, ProjectEnd end)
    {
        if (end.Value >= start.Value)
        {
            this.Start = start;
            this.End = end;
        }
        else
        {
            throw new ArgumentException("Project end date must be greater than or equal to start date");
        }
    }

    public void UpdateStatus(ProjectStatus value)
    {
        if (Enum.IsDefined(value))
        {
            this.Status = value;
        }
        else
        {
            throw new ArgumentException("Invalid project status value", nameof(value));
        }
    }
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
            return Validation.Invalid($"{nameof(ProjectName)} cannot be empty");
        }
        else if (value.Length > ValidationConstants.DefaultTextLength)
        {
            return Validation.Invalid($"{nameof(ProjectName)} exceeds maximum length of {ValidationConstants.DefaultTextLength} characters");
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

    [GeneratedRegex(ValidationConstants.HexColorRegex)]
    private static partial Regex HexColorRegex();

    internal static Validation Validate(string value)
    {
        if (value is null)
        {
            return Validation.Invalid($"{nameof(ProjectColor)} cannot be null");
        }
        else if (!string.IsNullOrEmpty(value) && !HexColorRegex().IsMatch(value))
        {
            return Validation.Invalid($"{nameof(ProjectColor)} is not a valid hex color");
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
            return Validation.Invalid($"{nameof(ProjectDescription)} cannot be null");
        }
        else if (value.Length > ValidationConstants.DefaultTextLength)
        {
            return Validation.Invalid($"{nameof(ProjectDescription)} exceeds maximum length of {ValidationConstants.DefaultTextLength} characters");
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

[ValueObject<DateTime>]
public readonly partial struct ProjectStart
{
    public static readonly ProjectStart Unspecified = new(DateTime.MinValue);
}

[ValueObject<DateTime>]
public readonly partial struct ProjectEnd
{
    public static readonly ProjectEnd Unspecified = new(DateTime.MaxValue);
}
