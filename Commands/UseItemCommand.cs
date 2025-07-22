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
/// Represents a command to use an item from the player's inventory within the game
/// </summary>
public class UseItemCommand(GameManager game) : BaseCommand, IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public override string Name => "Use";

    /// <inheritdoc />
    public override string Description => "Use an item from your inventory";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (GetAvailability())
        {
            game.Player.Backpack.Pop().Use(game);
            game.IncrementActionsCount();

        }
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability()
    {
        game.Player.Backpack.TryPeek(out IStorable<Room>? item);

        return item != null;
    }

    #endregion
}