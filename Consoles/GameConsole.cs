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
    #region Private Fields

    /// <summary>
    /// Represents the default delay, in milliseconds, between character outputs
    /// </summary>
    private const int CharDelay = 10;

    /// <summary>
    /// Represents the multiplier for the delay when a new line character is encountered
    /// </summary>
    private const int NewLineMultiplier = 100;

    #endregion

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
        Write(message.Trim() + "\n");
    }

    /// <inheritdoc />
    public void Write(string message)
    {
        GameLogger.Log(message);

        // Animate text character by character
        for (int i = 0; i < message.Length; i++)
        {
            Console.Write(message[i]);

            // Check if a key has been pressed
            if (Console.KeyAvailable)
            {
                // If key pressed, write the remaining text immediately
                if (i < message.Length - 1)
                {
                    Console.Write(message[(i + 1)..]);
                }
                break;
            }

            if (message[i] == '\n')
            {
                Thread.Sleep(CharDelay * NewLineMultiplier);
            }
            else
            {
                Thread.Sleep(CharDelay);
            }
        }
    }

    /// <inheritdoc />
    public string ReadLine(string prompt = "")
    {
        if (!string.IsNullOrEmpty(prompt))
        {
            Write(prompt);
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
                Write(prompt);
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
