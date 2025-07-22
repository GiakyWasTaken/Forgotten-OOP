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
    "Un occhio del guardiano, quando Lui si avvicina, l’Occhio indica con precisione in quale stanza adiacente si nasconde. E' uno strumento molto potente, usalo con saggezza",
    6.0f), IStorable<Room>, IConsolable, ILoggable
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
                Dictionary<Direction, Room?> adj = enemy.CurrentRoom.GetAdjacentRooms();

                for (int i = 0; i < adj.Count; i++)
                {
                    var dir = (Direction)i;

                    if (Equals(adj[dir]?.GetCoordinates(), game.Player.CurrentRoom.GetCoordinates()))
                    {
                        GameConsole.WriteLine("Il Guardiano vede ciò che gli altri non possono: L’Ushigami si trova nella stanza a " + (Direction)(5 - i));
                    }
                }
            }
        });

        GameLogger.Log("Player used Guardian's Eye");

        game.IncrementActionsCount();
    }

    #endregion
}
