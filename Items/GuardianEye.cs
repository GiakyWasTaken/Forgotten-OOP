namespace Forgotten_OOP.Items;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities;
using Forgotten_OOP.Entities.Interfaces;
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
    "Un occhio del guardiano, quando l'Ushigami si avvicina, l’Occhio indica con precisione in quale stanza adiacente si nasconde. E' uno strumento molto potente, usalo con saggezza"), IStorable<Room>, IConsolable, ILoggable
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
            if (entity is not IEnemy<Room> enemy)
            {
                return;
            }

            Dictionary<Direction, Room> adj = enemy.CurrentRoom.GetAdjacentRooms();

            foreach (KeyValuePair<Direction, Room> pair in adj.Where(pair => pair.Value.Equals(game.Player.CurrentRoom)))
            {
                game.Player.Backpack.Pop();
                GameConsole.WriteLine("Il Guardiano vede ciò che gli altri non possono: l’Ushigami si trova nella stanza verso " + DirectionsHelper.DirectionToString(pair.Key));
                return;
            }

            GameConsole.WriteLine("L’Occhio del Guardiano non vede nulla, l’Ushigami non si trova in una stanza adiacente");
        });
        
        GameLogger.Log("Player used Guardian's Eye");
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
