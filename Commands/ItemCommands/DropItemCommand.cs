namespace Forgotten_OOP.Commands.ItemCommands;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;

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
            GameConsole.WriteLine("Non posso dropparlo"); //TODO Find a better line
            return;
        }

        game.Player.Backpack.Pop().Drop(game.Player.CurrentRoom);
        game.IncrementActionsCount();
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability()
    {
        IItem itemInBag = game.Player.Backpack.Peek();

        return itemInBag is IDroppable<Room>;
    }

    #endregion
}