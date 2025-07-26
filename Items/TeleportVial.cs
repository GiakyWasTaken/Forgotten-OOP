namespace Forgotten_OOP.Items;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities;
using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Represents a teleportation vial item that, when used,
/// instantly transports the player to a random room in the dungeon
/// </summary>
public class TeleportVial()
    : Item("Fiala del Teletrasporto",
        "Una pozione che trasporta istantaneamente chi la beve in una stanza casuale del dungeon. Se un compagno si trova con te, ti seguirà automaticamente"), IStorable<Room>, IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public float Weight => 4.0f;

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

        GameLogger.Log("Player used Teleport Vial");
        GameLogger.Log("Player teleported to room " + game.Player.CurrentRoom);

        game.Player.FollowingEntities.ForEach(entity =>
        {
            entity.Teleport(game.Player.CurrentRoom);

            GameLogger.Log($"{entity.Name} teleported to room {game.Player.CurrentRoom}");
        });

        GameConsole.WriteLine("Chiudi gli occhi per un secondo, senti un soffio di vento. Quando li riapri, ti trovi in una stanza diversa");
    }

    /// <inheritdoc />
    public void Grab(Player player)
    {
        player.Backpack.Push(this);
        GameLogger.Log($"{Name} è stato aggiunto allo zaino.");
        GameConsole.WriteLine($"Hai raccolto: {Name}");
        GameConsole.WriteLine(Description);
    }

    #endregion
}