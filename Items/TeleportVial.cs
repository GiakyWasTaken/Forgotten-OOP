namespace Forgotten_OOP.Items;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities;
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
        "Una pozione che trasporta istantaneamente chi la beve in una stanza casuale del dungeon. Se un compagno si trova con te, ti seguirà automaticamente",
        4.0f), IStorable<Room>, IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void Grab(GameManager game)
    {
        game.Player.Backpack.Push(this);
        game.GameLogger.Log($"{Name} è stato aggiunto allo zaino.");
        GameConsole.WriteLine($"Hai raccolto: {Name}"); ;
    }

    /// <inheritdoc />
    public override void Use(GameManager game)
    {
        game.Entities.ForEach(entity =>
        {
            if (entity is Enemy enemy)
            {
                bool check = true;

                while (check)
                {
                    Room teleportTo = game.GameMap.GetRandomRoom();

                    if (!teleportTo.IsPinkRoom && !teleportTo.Equals(game.Player.CurrentRoom) && !teleportTo.Equals(enemy.CurrentRoom))
                    {
                        game.Player.Teleport(teleportTo);
                        check = false;
                    }
                }
            }
        });

        game.Player.Teleport(game.GameMap.GetRandomRoom());

        GameLogger.Log("Player used Teleport Vial");
        GameLogger.Log("Player teleported to room " + game.Player.CurrentRoom);

        game.IncrementActionsCount();

        GameConsole.WriteLine("Chiudi gli occhi per un secondo, senti un soffio di vento. Quando li riapri, ti trovi in una stanza diversa");
    }
    #endregion
}