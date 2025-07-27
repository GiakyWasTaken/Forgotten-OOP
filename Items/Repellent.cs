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
/// Represents a repellent item that can be used to cause enemies to move away
/// </summary>
public class Repellent() : Item("Scaccia-Presenze", "Se usato, l'Ushigami si allontanera' rapidamente"), IStorable<Room>,
    IConsolable, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public float Weight => 4.0f;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Use(GameManager game)
    {
        game.Entities.ForEach(entity =>
        {
            if (entity is not IEnemy<Room> enemy)
            {
                return;
            }

            Room teleportTo = game.GameMap.GetRandomRoom(room =>
                !(
                    room.IsPinkRoom
                    || room.IsStartingRoom
                    || room.Equals(game.Player.CurrentRoom)
                ));

            enemy.Teleport(teleportTo);

            GameLogger.Log("Player used Repellent");
            GameLogger.Log($"{enemy.Name} moved to room {enemy.CurrentRoom}");

        });
        game.Player.Backpack.Pop();
        GameConsole.WriteLine("La miscela si disperde. Lo senti allontanarsi");
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