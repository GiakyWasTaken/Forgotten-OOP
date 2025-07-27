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
/// Represents a healing bandage item that can be stored, logged, and used within the game.
/// </summary>
public class Bandage() : Item("Bende", "Delle bende curative, permettono di recuperare una vita"), IStorable<Room>,
    IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public float Weight => 2.0f;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Use(GameManager game)
    {
        if (game.Player.Lives < 3)
        {
            game.Player.Lives++;

            GameLogger.Log("Player used bandages");
            game.Player.Backpack.Pop();
            GameConsole.WriteLine("La ferita si chiude... mi sento molto meglio");
        }
        else
        {
            GameConsole.WriteLine("Non sono ferito, meglio usarla quando mi servira'");
        }
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
