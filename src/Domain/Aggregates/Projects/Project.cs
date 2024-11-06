using Vogen;

namespace Etn.MyLittleBoard.Domain.Aggregates.Projects;

public sealed class Project() : EntityBase<Project, ProjectId>
{
    public Project(ProjectName name) : this()
    {
        this.Name = name;
    }

    public ProjectName Name { get; private set; }
}

[ValueObject<int>]
public readonly partial struct ProjectId;

[ValueObject<string>]
public readonly partial struct ProjectName
{
    internal static Validation Validate(in string name)
    {
        return string.IsNullOrWhiteSpace(name) ? Validation.Invalid("Name cannot be empty") : Validation.Ok;
    }

    internal static string NormalizeInput(string name)
    {
        return name.Trim();
    }
}
