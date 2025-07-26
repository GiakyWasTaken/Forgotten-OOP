namespace Forgotten_OOP.Commands.Interfaces;

/// <summary>
/// Interface representing a command in the game
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Gets the name associated with the command
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the description of the command
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the item is available
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Executes the command with the given game manager
    /// </summary>
    public void Execute();
}