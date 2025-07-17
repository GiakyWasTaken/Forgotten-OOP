namespace Forgotten_OOP.Consoles.Interfaces;

#region Using Directives

using System.Collections.Generic;

using Forgotten_OOP.Commands.Interfaces;

#endregion

/// <summary>
/// An interface for a helper that provides commands and help information for a console application
/// </summary>
public interface IConsoleHelper<TCommand> where TCommand : ICommand
{
    /// <summary>
    /// A list of commands that the helper can execute
    /// </summary>
    public List<TCommand> Commands { get; set; }

    /// <summary>
    /// Prints the help information and the available commands to the console
    /// </summary>
    public void PrintHelp();
}
