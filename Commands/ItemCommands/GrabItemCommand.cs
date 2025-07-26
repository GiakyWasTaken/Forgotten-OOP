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
/// Represents a command to grab an item from the within the game
/// </summary>
public class GrabItemCommand(GameManager game) : BaseCommand, IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public override string Name => "Grab";

    /// <inheritdoc />
    public override string Description => "Grab an item on the floor";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (!GetAvailability(out string tryExecutionMessage))
        {
            GameConsole.WriteLine(tryExecutionMessage);
            GameLogger.Log("Player tried to grab an item, but it wasn't possible");
            return;
        }

        IItem itemToGrab = game.Player.CurrentRoom.ItemsOnGround.Peek();

        if (itemToGrab is IGrabbable grabbable)
        {
            game.Player.CurrentRoom.ItemsOnGround.Pop();
            grabbable.Grab(game.Player);
            game.IncrementActionsCount();
        }
        else
        {
            GameConsole.WriteLine(tryExecutionMessage);
            GameLogger.Log("Player tried to grab an item, but it wasn't grabbable");
        }
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability(out string tryExecutionMessage)
    {
        tryExecutionMessage = string.Empty;
        if (game.Player.CurrentRoom.ItemsOnGround.Count == 0)
        {
            tryExecutionMessage = "Non c'è nulla da raccogliere";
            return false;
        }

        IItem itemOnFloor = game.Player.CurrentRoom.ItemsOnGround.Peek();

        if (itemOnFloor is not IStorable<Room> storableItem || game.Player.GetCurrentWeight() + storableItem.Weight <= 10)
        {
            return true;
        }

        tryExecutionMessage = "Non posso raccogliere questo oggetto, il mio zaino diventerà troppo pesante";
        return false;
    }

    #endregion
}