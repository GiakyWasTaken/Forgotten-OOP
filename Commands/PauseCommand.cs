namespace Forgotten_OOP.Commands;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;

#endregion

/// <summary>
/// Represents a command to show help information in the Forgotten OOP game
/// </summary>
public class PauseCommand(GameManager game) : BaseCommand, IConsolable, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public override string Name => "Pause";

    /// <inheritdoc />
    public override string Description => "Mette in pausa la partita e ritorna al menu iniziale";

    /// <inheritdoc />
    public override bool IsAvailable => true;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        game.PauseGame();
    }

    #endregion
}