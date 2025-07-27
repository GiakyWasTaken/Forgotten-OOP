namespace Forgotten_OOP.Commands.ItemCommands;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Represents a command to grab an item from the within the game
/// </summary>
public class GrabItemCommand(GameManager game) : BaseCommand, IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public override string Name => "Grab";

    /// <inheritdoc />
    public override string Description => "Raccogli un oggetto dal pavimento";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (!GetAvailability(out string tryExecutionMessage))
        {
            GameConsole.WriteLine(tryExecutionMessage);
            GameLogger.Log("Player tried to grab an item, but it wasn't possible");
            return;
        }

        List<IItem> grabbableItems = [.. game.Player.CurrentRoom.ItemsOnGround];

        if (grabbableItems.Count == 1)
        {
            AttemptToGrabItem(grabbableItems[0]);
            return;
        }

        GameConsole.WriteLine("In questa stanza ci sono questi oggetti:");

        for (int i = 0; i < grabbableItems.Count; i++)
        {
            GameConsole.WriteLine($"{i + 1}. {grabbableItems[i].Name}");
        }

        int selectedIndex;
        while (true)
        {
            string input = GameConsole.ReadLine("Quale di questi oggetti prendo? ");

            if (int.TryParse(input, out selectedIndex) && selectedIndex > 0 && selectedIndex <= grabbableItems.Count)
            {
                break;
            }

            GameConsole.WriteLine("Non ho capito, per favore inserisci un oggetto valido");
            //TODO: aggiungere una opzione per uscire dal menu grab
        }

        IItem selectedItem = grabbableItems[selectedIndex - 1];
        AttemptToGrabItem(selectedItem);
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability(out string tryExecutionMessage)
    {
        tryExecutionMessage = string.Empty;

        List<IGrabbable> grabbableItems = [.. game.Player.CurrentRoom.ItemsOnGround.OfType<IGrabbable>()];

        if (game.Player.CurrentRoom.ItemsOnGround.Count == 0)
        {
            tryExecutionMessage = "Non c'è nulla da raccogliere";
            return false;
        }

        if (grabbableItems.Count == 0)
        {
            tryExecutionMessage = "Non posso raccogliere nessun oggetto qua";
            return false;
        }

        if (!grabbableItems.Any(item => item is not IStorable<Room> storable || storable.Weight + game.Player.GetCurrentWeight() <= 10))
        {
            tryExecutionMessage = "Non posso raccogliere nessun oggetto, il mio zaino sarà troppo pesante";
            return false;
        }

        return true;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Attempts to grab the specified item if it is grabbable
    /// </summary>
    /// <param name="itemToGrab">The item to attempt to grab. Must implement <see cref="IStorable{Room}"/> to be successfully grabbed</param>
    private void AttemptToGrabItem(IItem itemToGrab)
    {
        if (itemToGrab is IStorable<Room> grabbable)
        {
            if (grabbable.Weight + game.Player.GetCurrentWeight() > 10)
            {
                GameConsole.WriteLine("Non posso raccogliere questo oggetto, il mio zaino sarà troppo pesante");
                GameLogger.Log("Player tried to grab an item, but it was too heavy to fit in the backpack");
                return;
            }

            game.Player.CurrentRoom.ItemsOnGround.Remove(itemToGrab);
            grabbable.Grab(game.Player);
            game.IncrementActionsCount();

            GameConsole.WriteLine($"Ho preso {itemToGrab.Name} e l'ho messo dentro lo zaino");
            GameLogger.Log($"Player grabbed the {itemToGrab.Name}");
        }
        else
        {
            GameConsole.WriteLine("Non posso raccogliere nessun oggetto qua");
            GameLogger.Log("Player tried to grab an item, but it wasn't grabbable.");
        }
    }

    #endregion
}