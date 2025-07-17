namespace Forgotten_OOP.Commands;

#region Using Directives

using Forgotten_OOP.Enums;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Represents a command to use an item from the player's inventory within the game
/// </summary>
public class PlayerMoveCommand(GameManager game, Direction dir) : BaseCommand
{
    #region Properties

    /// <inheritdoc />
    public override string Name => "Use";

    /// <inheritdoc />
    public override string Description => "Use an item from your inventory";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (GetAvailability())
        {
            game.Player.Move(dir);
            game.IncrementActionsCount();
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