namespace Forgotten_OOP.Consoles.Interfaces;

#region Using Directives

using System.Collections.Generic;

using Forgotten_OOP.Commands.Interfaces;

#endregion

/// <summary>
/// An interface for console operations
/// </summary>
public interface IConsole
{
    /// <summary>
    /// A list of commands that the helper can execute
    /// </summary>
    public List<ICommand> Commands { get; set; }

    /// <summary>
    /// Writes a message to the console
    /// </summary>
    /// <param name="message">A string to display in the console</param>
    /// <param name="skipWriteAnimation">A boolean indicating whether to skip the write animation</param>
    public void Write(string message, bool skipWriteAnimation = false);

    /// <summary>
    /// Writes a message to the console with a newline at the end
    /// </summary>
    /// <param name="message">A string to display in the console</param>
    /// <param name="skipWriteAnimation">A boolean indicating whether to skip the write animation</param>
    public void WriteLine(string message = "", bool skipWriteAnimation = false);

    /// <summary>
    /// Reads a line from the console with a prompt
    /// </summary>
    /// <param name="prompt">A string to display before reading input</param>
    /// <param name="skipWriteAnimation">A boolean indicating whether to skip the write animation</param>
    /// <returns>The input string from the console</returns>
    public string ReadLine(string prompt = "", bool skipWriteAnimation = false);

    /// <summary>
    /// Reads and returns the next command from the input stream
    /// </summary>
    /// <param name="prompt">A string to display before reading the command</param>
    /// <param name="skipWriteAnimation">A boolean indicating whether to skip the write animation</param>
    /// <returns>A <see cref="ICommand"/> object representing the next command</returns>
    public ICommand ReadCommand(string prompt = "", bool skipWriteAnimation = false);

    /// <summary>
    /// Clears the console output
    /// </summary>
    public void Clear();

    /// <summary>
    /// Prints the help information and the available commands to the console
    /// </summary>
    public void PrintHelp();
}
