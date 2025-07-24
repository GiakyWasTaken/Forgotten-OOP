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
/// Represents a command to drop an item from the within the game
/// </summary>
public class InspectItemCommand(GameManager game) : BaseCommand, IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Properties

    /// <inheritdoc />
    public override string Name => "Inspect";

    /// <inheritdoc />
    public override string Description => "Insepct an item in your bag";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (!GetAvailability())
        {
            GameConsole.WriteLine("Non posso Ispezionarlo"); //TODO Find a better line
            return;
        }

        GameConsole.WriteLine(game.Player.Backpack.Peek().Description);
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability()
    {
        return game.Player.Backpack.Peek().Description != null;
    }

    #endregion
}