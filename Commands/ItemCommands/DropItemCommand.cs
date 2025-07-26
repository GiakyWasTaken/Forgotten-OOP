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
public class DropItemCommand(GameManager game) : BaseCommand, IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public override string Name => "Drop";

    /// <inheritdoc />
    public override string Description => "Drop an item on the floor";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (!GetAvailability())
        {
            // Todo: review this message
            GameConsole.WriteLine("Non posso perdere questo oggetto...");
            GameLogger.Log("Player tried to drop an item, but it wasn't droppable possible.");
            return;
        }

        game.Player.Backpack.Pop().Drop(game.Player.CurrentRoom);

        GameLogger.Log("Player dropped and item");
        game.IncrementActionsCount();
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability()
    {
        return game.Player.Backpack.Count > 0;
    }

    #endregion
}