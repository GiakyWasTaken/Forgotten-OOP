namespace Forgotten_OOP.Injectables;

#region Using Directives

using Forgotten_OOP.Injectables.Interfaces;

#endregion

/// <summary>
/// A console wrapper for the Forgotten OOP game
/// </summary>
public class GameConsole : IConsole
{
    #region Public Methods

    /// <inheritdoc />
    public void WriteLine(string message)
    {
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
        Console.Write(prompt);

        return Console.ReadLine() ?? string.Empty;
    }

    /// <inheritdoc />
    public void Clear()
    {
        Console.Clear();
    }

    #endregion
}
