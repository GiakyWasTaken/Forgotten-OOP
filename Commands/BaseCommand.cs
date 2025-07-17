namespace Forgotten_OOP.Commands;

#region Using Directives

using Forgotten_OOP.Commands.Interfaces;

#endregion

/// <summary>
/// Represents a command that can be executed within the game context
/// </summary>
public abstract class BaseCommand : ICommand
{
    #region Private Fields

    /// <summary>
    /// Indicates whether the command is currently available for execution
    /// </summary>
    protected bool? ForceIsAvailable;

    #endregion

    #region Properties

    /// <inheritdoc />
    public abstract string Name { get; }

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public virtual bool IsAvailable
    {
        get => ForceIsAvailable ?? GetAvailability();
        set => ForceIsAvailable = value;
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public abstract void Execute();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Name}: {Description}";
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Determines the availability of the command based on game state or conditions
    /// </summary>
    /// <returns>True if the command is available, otherwise false</returns>
    protected abstract bool GetAvailability();

    #endregion
}