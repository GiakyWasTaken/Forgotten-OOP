namespace Forgotten_OOP.Items;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities;
using Forgotten_OOP.Enums;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Represents a powerful tool that reveals the location of an enemy in an adjacent room
/// </summary>
public class GuardianEye() : Item("Occhio del Guardiano",
    "Un occhio del guardiano, quando Lui si avvicina, l’Occhio indica con precisione in quale stanza adiacente si nasconde. E' uno strumento molto potente, usalo con saggezza"), IStorable<Room>, IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public float Weight => 6.0f;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Use(GameManager game)
    {
        game.Entities.ForEach(entity =>
        {
            if (entity is not Enemy enemy)
            {
                return;
            }

            Dictionary<Direction, Room> adj = enemy.CurrentRoom.GetAdjacentRooms();

            for (int i = 0; i < adj.Count; i++)
            {
                var dir = (Direction)i;

                if (Equals(adj[dir]?.GetCoordinates(), game.Player.CurrentRoom.GetCoordinates()))
                {
                    GameConsole.WriteLine("Il Guardiano vede ciò che gli altri non possono: L’Ushigami si trova nella stanza a " + (Direction)(5 - i));
                }
            }
        });

        GameLogger.Log("Player used Guardian's Eye");

        game.IncrementActionsCount();
    }

    /// <inheritdoc />
    public void Grab(Player player)
    {
        player.Backpack.Push(this);
        GameLogger.Log($"{Name} è stato aggiunto allo zaino.");
        GameConsole.WriteLine($"Hai raccolto: {Name}");
        GameConsole.WriteLine(this.Description);
    }

    #endregion
}
