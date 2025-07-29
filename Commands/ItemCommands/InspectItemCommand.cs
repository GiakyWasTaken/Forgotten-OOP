namespace Forgotten_OOP.Commands.ItemCommands;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;

#endregion

/// <summary>
/// Represents a command to drop an item from the within the game
/// </summary>
public class InspectItemCommand(GameManager game) : BaseCommand, IConsolable, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public override string Name => "Inspect";

    /// <inheritdoc />
    public override string Description => "Ispeziona l'ultimo oggetto inserito nello zaino";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (!GetAvailability(out string tryExecutionMessage))
        {
            GameConsole.WriteLine(tryExecutionMessage);
            GameLogger.Log("Player tried to Inspect an item, but it wasn't possible");
            return;
        }

        GameConsole.WriteLine(game.Player.Backpack.Peek().Description);
        GameLogger.Log("Player Inspected an Item");
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability(out string tryExecutionMessage)
    {
        tryExecutionMessage = string.Empty;
        if (game.Player.Backpack.Count > 0)
        {
            return true;
        }

        tryExecutionMessage = "Non ho niente nello zaino";
        return false;
    }

    #endregion
}