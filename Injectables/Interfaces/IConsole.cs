namespace Forgotten_OOP.Injectables.Interfaces;

/// <summary>
/// An interface for console operations
/// </summary>
public interface IConsole
{
    /// <summary>
    /// Writes a message to the console
    /// </summary>
    /// <param name="message">A string to display in the console</param>
    public void WriteLine(string message);

    /// <summary>
    /// Reads a line from the console
    /// </summary>
    /// <returns>The input string from the console</returns>
    public string ReadLine();

    /// <summary>
    /// Reads a line from the console with a prompt
    /// </summary>
    /// <param name="prompt">A string to display before reading input</param>
    /// <returns>The input string from the console</returns>
    public string ReadLine(string prompt);

    /// <summary>
    /// Clears the console output
    /// </summary>
    public void Clear();
}
