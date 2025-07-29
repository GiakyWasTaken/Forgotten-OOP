namespace Forgotten_OOP.Commands.ItemCommands;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;

#endregion

/// <summary>
/// Represents a command to use an item from the player's inventory within the game
/// </summary>
public class UseItemCommand(GameManager game) : BaseCommand, IConsolable, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public override string Name => "Use";

    /// <inheritdoc />
    public override string Description => "Usa l'ultimo oggetto inserito nello zaino o un oggetto a terra";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (!GetAvailability(out string tryExecutionMessage))
        {
            GameConsole.WriteLine(tryExecutionMessage);
            GameLogger.Log("Player tried to use an item, but it wasn't possible");
            return;
        }

        List<IItem> usableItems = [game.Player.Backpack.Peek(), .. game.Player.CurrentRoom.ItemsOnGround.Where(item => item is not IGrabbable)];

        if (usableItems.Count == 1)
        {
            AttemptToUseItem(usableItems[0]);
            return;
        }

        GameConsole.WriteLine("In questa stanza e in cima al mio zaino ci sono questi oggetti che posso usare:");

        for (int i = 0; i < usableItems.Count; i++)
        {
            GameConsole.WriteLine($"{i + 1}. {usableItems[i].Name}");
        }

        int selectedIndex;

        while (true)
        {
            if (int.TryParse(GameConsole.ReadLine("Quale di questi oggetti uso? "), out selectedIndex) && selectedIndex > 0 && selectedIndex <= usableItems.Count)
            {
                break;
            }

            GameConsole.WriteLine("Non ho capito, per favore inserisci un oggetto valido");
        }

        IItem selectedItem = usableItems[selectedIndex - 1];
        AttemptToUseItem(selectedItem);
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability(out string tryExecutionMessage)
    {
        tryExecutionMessage = string.Empty;

        if (game.Player.Backpack.Count <= 0)
        {
            if (game.Player.CurrentRoom.ItemsOnGround.Count > 0)
            {
                if (game.Player.CurrentRoom.ItemsOnGround.Any(item => item is not IGrabbable))
                {
                    return true;
                }

                tryExecutionMessage = "Non ho niente nello zaino e non posso usare nessun oggetto a terra";
            }

            tryExecutionMessage = "Non ho niente nello zaino";
            return false;
        }

        return true;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Attempts to use the specified item if it is usable
    /// </summary>
    /// <param name="itemToUse">The item to attempt to use. Must implement <see cref="IItem"/> to be successfully used</param>
    private void AttemptToUseItem(IItem itemToUse)
    {
        GameConsole.WriteLine($"Ho usato {itemToUse.Name}");
        GameLogger.Log($"Player used the {itemToUse.Name}");

        itemToUse.Use(game);
        game.IncrementActionsCount();
    }

    #endregion
}