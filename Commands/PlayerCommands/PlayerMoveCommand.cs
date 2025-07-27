namespace Forgotten_OOP.Commands.PlayerCommands;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Enums;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Represents a command to move the player to a cardinal within the game map
/// </summary>
public class PlayerMoveCommand(GameManager game, Direction direction) : BaseCommand, IConsolable, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public override string Name { get; } = DirectionsHelper.DirectionToString(direction);

    /// <inheritdoc />
    public override string Description { get; } = $"Muoviti verso {DirectionsHelper.DirectionToString(direction).ToLowerInvariant()}";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (!GetAvailability(out string tryExecutionMessage))
        {
            GameConsole.WriteLine(tryExecutionMessage);
            return;
        }

        if (!game.GameMap.TryGetRoomInDirection(game.Player.CurrentRoom, direction, out Room? room) || room is null)
        {
            return;
        }

        if (room.IsClosed)
        {
            var key = game.Player.KeyItems.OfType<Key>().FirstOrDefault();

            if (key is null)
            {
                return;
            }

            game.Player.KeyItems.Remove(key);

            GameLogger.Log($"Player used a key to open the door of the room {room}");

            GameConsole.WriteLine("La porta è chiusa, ma sono riuscito ad aprirla con la chiave che ho trovato");
        }

        game.Player.Move(direction);

        GameConsole.WriteLine("Mi sono spostato in una nuova stanza");

        switch (game.Player.CurrentRoom.ItemsOnGround.OfType<IGrabbable>().Count())
        {
            case 0:
                GameConsole.WriteLine("Non c'è nulla in questa stanza");
                break;
            case 1:
                GameConsole.WriteLine("Vedo qualcosa per terra nella penombra, forse dovrei raccoglierlo");
                break;
            default:
                GameConsole.WriteLine("Ci sono degli oggetti per terra, forse dovrei raccoglierli");
                break;
        }

        int foundTorch = -1, foundKey = -1;
        
        for(int i=0; i< game.Player.CurrentRoom.ItemsOnGround.Count; i++)
        {
            IItem item = game.Player.CurrentRoom.ItemsOnGround[i];

            if (item is Torch)
            {
                GameConsole.WriteLine("Osservando meglio la stanza c'e' una fioca luce proveniente dal centro di essa. E una torcia! con questa posso entrare nelle stanze più buie! Meglio ricontrollare la mappa");
                GameConsole.WriteLine("Hai raccolto la Torcia");
                game.Player.KeyItems.Add((IKeyItem)item);
                GameLogger.Log("Player grabbed Torch");
                foundTorch = i;

            }
            if (item is Key)
            {
                GameConsole.WriteLine("Questa stanza è più stretta delle altre, grazie alla torcia posso vedere tutto chiaramente. In fondo, appesa ad un muro, sembrerebbe esserci una chiave dorata. Non posso lasciarla li', mi tornera' sicuramente utile.");
                GameConsole.WriteLine("Hai raccolto la Chiave");
                game.Player.KeyItems.Add((IKeyItem)item);
                GameLogger.Log("Player grabbed Key");
                foundKey = i;
            }
        }
        if(foundTorch > -1)
        {
            game.Player.CurrentRoom.ItemsOnGround.RemoveAt(foundTorch);
        }
        if (foundKey > -1)
        {
            game.Player.CurrentRoom.ItemsOnGround.RemoveAt(foundKey);
        }




        game.IncrementActionsCount();

        GameLogger.Log("Player moved in direction " + Name.ToLowerInvariant());
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability(out string tryExecutionMessage)
    {
        tryExecutionMessage = string.Empty;

        if (!game.GameMap.TryGetRoomInDirection(game.Player.CurrentRoom, direction, out Room? room) || room is null)
        {
            tryExecutionMessage = "Non posso andare in quella direzione";
            return false;
        }

        // Specific line for when the player encounters marlo and is forced to use the altar
        if (game.Player.CurrentRoom.IsClosed)
        {
            tryExecutionMessage = "La porta è bloccata dall'esterno, non posso uscire così.\n" +
                "Dai Marlo, usiamo l'altare che sta nell'angolo";
            return false;
        }

        if (room.IsClosed && !game.Player.KeyItems.OfType<Key>().Any())
        {
            tryExecutionMessage = "La porta è chiusa e adesso non riesco ad aprirla, però sento qualcuno parlare attraverso"; // Todo: check line
            return false;
        }

        return true;
    }

    #endregion
}