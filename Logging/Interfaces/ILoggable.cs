namespace Forgotten_OOP.Logging.Interfaces;

/// <summary>
/// Represents an entity that can perform logging operations using a game console interface
/// </summary>
public interface ILoggable
{
    /// <summary>
    /// Gets the logger used for recording game-related events and messages
    /// </summary>
    public ILogger GameLogger { get; }
}