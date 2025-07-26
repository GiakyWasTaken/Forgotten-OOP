namespace Forgotten_OOP.Commands;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Represents a command that displays the items in the player's backpack
/// </summary>
public class BackPackCommand(GameManager game) : BaseCommand, IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public override string Name => "Backpack";

    /// <inheritdoc />
    public override string Description => "Returns the items in your backpack ";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (game.Player.Backpack.Count == 0)
        {
            GameConsole.WriteLine("Ho controllato il mio zaino... ma è vuoto");
            GameLogger.Log("Player viewed backpack");
            return;
        }

        bool current = false;

        foreach (IStorable<Room> item in game.Player.Backpack)
        {
            GameConsole.Write(item.Name);

            if (!current)
            {
                GameConsole.WriteLine("<== oggetto in cima");
                current = true;
            }
        }

        GameLogger.Log("Player viewed backpack");
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability(out string tryExecutionMessage)
    {
        tryExecutionMessage = string.Empty;
        return true;
    }

    #endregion
}
