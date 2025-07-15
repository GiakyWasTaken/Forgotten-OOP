namespace Forgotten_OOP.Injectables.Interfaces;

#region Using Directives

using System.Collections.Generic;

#endregion

/// <summary>
/// An interface for a helper that provides commands and help information for a console application
/// </summary>
/// <typeparam name="T">Type of console that the helper will use</typeparam>
public interface IConsoleHelper<T> where T : IConsole
{
    /// <summary>
    /// A list of commands that the helper can execute
    /// </summary>
    // Todo: change from string to Command class
    public List<string> Commands { get; set; }

    /// <summary>
    /// Prints the help information and the available commands to the console
    /// </summary>
    public void PrintHelp();
}
