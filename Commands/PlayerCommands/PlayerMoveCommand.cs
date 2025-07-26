namespace Forgotten_OOP.Commands.PlayerCommands;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Enums;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Represents a command to move the player to a cardinal within the game map
/// </summary>
public class PlayerMoveCommand(GameManager game, Direction direction) : BaseCommand, IConsolable, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public override string Name { get; } = DirectionsHelper.DirectionToString(direction);

    /// <inheritdoc />
    public override string Description { get; } = $"Muoviti verso {DirectionsHelper.DirectionToString(direction).ToLowerInvariant()}";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (!GetAvailability(out string tryExecutionMessage))
        {
            GameConsole.WriteLine(tryExecutionMessage);
            return;
        }

        game.Player.Move(direction);
        game.IncrementActionsCount();
        GameLogger.Log("Player moved in direction " + Name.ToLowerInvariant());
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability(out string tryExecutionMessage)
    {
        tryExecutionMessage = string.Empty;
        game.GameMap.TryGetRoomInDirection(game.Player.CurrentRoom, direction, out Room? room);

        if (room is null)
        {
            tryExecutionMessage = "Non posso andare in quella direzione";
            return false;
        }

        if (room.IsClosed && !game.Player.KeyItems.Any(item => item is Key))
        {
            tryExecutionMessage = "La porta è chiusa e adesso non riesco ad aprirla, però sento qualcuno parlare attraverso"; // Todo: check line
            return false;
        }

        return true;
    }

    #endregion
}