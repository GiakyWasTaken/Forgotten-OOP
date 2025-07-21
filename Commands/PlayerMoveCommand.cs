namespace Forgotten_OOP.Commands;

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities;

#region Using Directives

using Forgotten_OOP.Enums;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents a command to use an item from the player's inventory within the game
/// </summary>
public class PlayerMoveCommand(GameManager game, Direction dir) : BaseCommand
{
    #region Private Fields
    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();
    #endregion

    #region Properties

    /// <inheritdoc />
    public override string Name => "Move Player";

    /// <inheritdoc />
    public override string Description => "Moves the player";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (GetAvailability())
        {
            game.Player.Move(dir);
            game.IncrementActionsCount();
            GameLogger.Log("Player moved in direction" + dir);
        }
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability()
    {
        game.GameMap.TryGetRoomInDirection(game.Player.CurrentRoom, dir, out Room? room);
        return room != null;
    }

    #endregion
}