namespace Forgotten_OOP.Items;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Represents a teleportation altar item that, when used,
/// instantly transports the player to a random room in the dungeon
/// </summary>
public class TeleportAltar()
    : Item("Altare del Teletrasporto",
        "Un altare che trasporta istantaneamente chi lo usa in una stanza casuale del dungeon. Se un compagno si trova con te, ti seguira' automaticamente"), IConsolable, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Use(GameManager game)
    {
        game.Player.Teleport(game.GameMap.GetRandomRoom(room =>
            !(
                room.IsPinkRoom
                || room.IsStartingRoom
                || room.Equals(game.Player.CurrentRoom)
                || game.Entities.OfType<IEnemy<Room>>().Any(enemy => enemy.CurrentRoom.Equals(room))
            )));

        GameLogger.Log("Player used Teleport Altar");
        GameLogger.Log("Player teleported to room " + game.Player.CurrentRoom);

        game.Player.FollowingEntities.ForEach(entity =>
        {
            entity.Teleport(game.Player.CurrentRoom);

            GameLogger.Log($"{entity.Name} teleported to room {game.Player.CurrentRoom}");
        });

        GameConsole.WriteLine("Chiudi gli occhi per un secondo, senti un soffio di vento. Quando li riapri, ti trovi in una stanza diversa");
    }

    #endregion
}