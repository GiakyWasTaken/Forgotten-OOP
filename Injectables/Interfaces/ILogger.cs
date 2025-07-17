using Forgotten_OOP.Mapping;

namespace Forgotten_OOP.Injectables.Interfaces;

/// <summary>
/// An interface for logging to file
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Initializes the logger
    /// </summary>
    public void InitializeLogger();

    /// <summary>
    /// Logs a message to the file
    /// </summary>
    /// <param name="message">A string to log</param>
    public void Log(string message);
}
