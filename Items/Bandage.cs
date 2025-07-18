namespace Forgotten_OOP.Items;


#region Using Directives

using Forgotten_OOP.Entities;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forgotten_OOP.Consoles.Interfaces;

#endregion

public class Bandage : Item, IStorable<Room>
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Constructor
    public Bandage(string name, string description, float weight) : base(name, description, weight)
    {
        name = "Bende";
        description = "Una benda curativa, permettono di recuperare una vita.";
        weight = 2.0f;
    }
    #endregion

    #region Public Methods
    void IGrabbable.Grab()
    {
        throw new NotImplementedException();
    }

    void IItem.Use(GameManager game)
    {
        if (game.Player.Lives < 3)
        {
            game.Player.Lives += 1;
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
