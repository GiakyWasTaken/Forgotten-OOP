namespace Forgotten_OOP.Consoles;

#region Using Directives

using System.Collections.Generic;

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;

#endregion

/// <summary>
/// A console wrapper for the Forgotten OOP game
/// </summary>
public class GameConsole : IConsole, IConsoleHelper<GameConsole>, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public List<string> Commands { get; set; } = [];

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void WriteLine(string message)
    {
        // Todo: Implement a cool feature like colored text or slow typing effect
        GameLogger.Log(message);
        Console.WriteLine(message);
    }

    /// <inheritdoc />
    public string ReadLine()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    /// <inheritdoc />
    public string ReadLine(string prompt)
    {
        GameLogger.Log(prompt);
        Console.Write(prompt);

        return Console.ReadLine() ?? string.Empty;
    }

    /// <inheritdoc />
    public void Clear()
    {
        Console.Clear();
    }

    /// <inheritdoc />
    public void PrintHelp()
    {
        WriteLine("Available commands:");
    }

    #endregion
}
