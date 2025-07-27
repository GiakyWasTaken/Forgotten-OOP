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
/// Represents a torch item in the game that allows the player to see the key and Takumi on the map
/// </summary>
public class Torch() : Item("Torcia", "Puoi vedere dove sta la chiave e Takumi dalla mappa"), IKeyItem, IConsolable, ILoggable
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

        GameLogger.Log($"{Name} è stato aggiunto agli oggetti chiave");
        GameConsole.WriteLine($"Hai raccolto: {Name}");
        GameConsole.WriteLine(Description);
    }

    #endregion
}