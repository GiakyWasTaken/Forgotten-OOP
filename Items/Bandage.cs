namespace Forgotten_OOP.Items;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

public class Bandage() : Item("Bende", "Una benda curativa, permettono di recuperare una vita.", 2.0f), IStorable<Room>,
    IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void Grab(GameManager game)
    {
        // Todo: remove game from grab
        game.Player.Backpack.Push(this);
        game.GameLogger.Log($"{Name} è stato aggiunto allo zaino.");
        GameConsole.WriteLine($"Hai raccolto: {Name}"); ;
    }

    /// <inheritdoc />
    public override void Use(GameManager game)
    {
        if (game.Player.Lives < 3)
        {
            game.Player.LifeChange(1, game);
            game.GameLogger.Log("Player used bandages");
            game.IncrementActionsCount();
            GameConsole.WriteLine("La ferita si chiude... mi sento molto meglio");
        }
        else
        {
            GameConsole.WriteLine("Non sono ferito, non c'è ne è bisogno");
        }
    }

    #endregion
}
