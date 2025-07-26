namespace Forgotten_OOP.Consoles;

#region Using Directives

using System.Collections.Generic;

using Forgotten_OOP.Commands.Interfaces;
using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;

#endregion

/// <summary>
/// A console wrapper for the Forgotten OOP game
/// </summary>
public class GameConsole : IConsole, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public List<ICommand> Commands { get; set; } = [];

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void WriteLine(string message)
    {
        Write(message.TrimEnd() + "\n");
    }

    /// <inheritdoc />
    public void Write(string message)
    {
        // Todo: Implement a cool feature like colored text or slow typing effect
        GameLogger.Log(message);
        Console.Write(message);
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

        return ReadLine();
    }

    /// <inheritdoc />
    public ICommand ReadCommand()
    {
        ICommand? command;

        do
        {
            string input = ReadLine();

            command = Commands.FirstOrDefault(cmd => input.StartsWith(cmd.Name, StringComparison.InvariantCultureIgnoreCase));

            // Todo: implement a re prompt mechanism if command is null
        } while (command is not { IsAvailable: true });

        return command;
    }

    /// <inheritdoc />
    public ICommand ReadCommand(string prompt)
    {
        GameLogger.Log(prompt);
        Console.Write(prompt);

        return ReadCommand();
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

        foreach (ICommand command in Commands)
        {
            WriteLine($"{command}");
        }
    }

    #endregion
}
