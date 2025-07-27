namespace Forgotten_OOP.Commands;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;

#endregion

/// <summary>
/// Represents a command to show help information in the Forgotten OOP game
/// </summary>
public class HelpCommand : BaseCommand, IConsolable, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public override string Name => "Help";

    /// <inheritdoc />
    public override string Description => "Mostra la lista di tutti i comandi disponibili";

    /// <inheritdoc />
    public override bool IsAvailable => true;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        GameConsole.PrintHelp();
    }

    #endregion
}