using System;
using System.Collections.Generic;
using System.Linq;
namespace Forgotten_OOP.Items;

using Forgotten_OOP.Consoles;
using Forgotten_OOP.Consoles.Interfaces;

#region Using Directives

using Forgotten_OOP.Entities;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;
using System.Text;
using System.Threading.Tasks;

#endregion

public class Repellent : Item, IStorable<Room>
{
    #region Private Fields
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();
    #endregion

    #region Constructor
    public Repellent(string name, string description, float weight) : base(name, description, weight)
    {
        name = "Scacciapresenze";
        description = "Se usato, Lui si allontanerà rapidamente";
        weight = 4.0f;
    }
    #endregion

    #region Public Methods
    void IGrabbable.Grab()
    {
        throw new NotImplementedException();
    }

    void IItem.Use(GameManager game)
    {
        game.Entities.ForEach(entity =>
        {
            if (entity is Enemy enemy)
            {
                Room teleportTo;
                bool check = true;
                while (check)
                {
                    teleportTo = game.GameMap.GetRandomRoom();
                    if (!teleportTo.IsPinkRoom && !teleportTo.Equals(game.Player.CurrentRoom))
                    {
                        enemy.Teleport(teleportTo);
                        check = false;
                    }
                }
                GameLogger.Log("Player used Repellent");
                GameLogger.Log($"{enemy.Name} moved to room {enemy.CurrentRoom}");
                game.IncrementActionsCount();
                GameConsole.WriteLine("La miscela si disperde. Lo senti allontanarsi");
                
            }
        });
    }
    #endregion
}