namespace Forgotten_OOP.Consoles.Interfaces;

/// <summary>
/// Represents an interface for objects that interact with a game console for input and output operations
/// </summary>
public interface IConsolable
{
    /// <summary>
    /// Gets the game console interface used for input and output operations
    /// </summary>
    public IConsole GameConsole { get; }
}