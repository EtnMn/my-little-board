using Etn.MyLittleBoard.Domain.Constants;
using Etn.MyLittleBoard.Domain.Interfaces;
using Vogen;

namespace Etn.MyLittleBoard.Domain.Aggregates.Clients;

public sealed class Client(
    ClientName name,
    ClientNote note) :
    EntityBase<Client, ClientId>, IAggregateRoot
{
    public ClientName Name { get; private set; } = name;

    public ClientNote Note { get; private set; } = note;

    public ClientState State { get; private set; } = ClientState.Enabled;

    public void Enable()
    {
        this.State = ClientState.Enabled;
    }

    public void Disable()
    {
        this.State = ClientState.Disabled;
    }

    public void UpdateName(ClientName value)
    {
        this.Name = value;
    }

    public void UpdateNote(ClientNote value)
    {
        this.Note = value;
    }
}

[ValueObject<int>]
public sealed partial class ClientId;

[ValueObject<string>]
public readonly partial struct ClientName
{
    internal static Validation Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Validation.Invalid($"{nameof(ClientName)} cannot be empty");
        }
        else if (value.Length > ValidationConstants.DefaultTextLength)
        {
            return Validation.Invalid($"{nameof(ClientName)} exceeds maximum length of {ValidationConstants.DefaultTextLength} characters");
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
public readonly partial struct ClientNote
{
    public static readonly ClientNote Unspecified = new(string.Empty);

    internal static Validation Validate(string value)
    {
        if (value is null)
        {
            return Validation.Invalid($"{nameof(ClientNote)} cannot be null");
        }
        else if (value.Length > ValidationConstants.LongTextLength)
        {
            return Validation.Invalid($"{nameof(ClientNote)} exceeds maximum length of {ValidationConstants.LongTextLength} characters");
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

[ValueObject<bool>]
public readonly partial struct ClientState
{
    public static readonly ClientState Enabled = new(true);
    public static readonly ClientState Disabled = new(false);
}
