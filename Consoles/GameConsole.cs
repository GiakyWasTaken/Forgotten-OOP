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
    public void WriteLine(string message = "")
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
    public string ReadLine(string prompt = "")
    {
        if (!string.IsNullOrEmpty(prompt))
        {
            GameLogger.Log(prompt);
            Console.Write(prompt);
        }

        return Console.ReadLine() ?? string.Empty;
    }

    /// <inheritdoc />
    public ICommand ReadCommand(string prompt = "")
    {
        ICommand? command;

        do
        {
            if (!string.IsNullOrEmpty(prompt))
            {
                GameLogger.Log(prompt);
                Console.Write(prompt);
            }

            string input = ReadLine();

            command = Commands.FirstOrDefault(cmd => input.StartsWith(cmd.Name, StringComparison.InvariantCultureIgnoreCase));

            if (command == null)
            {
                WriteLine("Non so cosa significa...");
            }
            else if (!command.IsAvailable)
            {
                command.Execute();
            }
        } while (command is not { IsAvailable: true });

        return command;
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
