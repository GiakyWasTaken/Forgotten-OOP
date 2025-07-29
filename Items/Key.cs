namespace Forgotten_OOP.Items;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;

#endregion

/// <summary>
/// Represents a key item that can be used to open locked rooms in the game
/// </summary>
public class Key() : Item("Chiave", "Puoi aprire stanze chiuse"), IKeyItem, IConsolable, ILoggable
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
    }

    /// <inheritdoc />
    public void Grab(Player player)
    {
        player.KeyItems.Add(this);

        GameLogger.Log($"{Name} has been added to the player's keyItems");
        GameConsole.WriteLine($"Hai raccolto: {Name}");
        GameConsole.WriteLine(Description);
    }

    #endregion
}