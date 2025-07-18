namespace Forgotten_OOP.Items;

using Forgotten_OOP.Consoles;
using Forgotten_OOP.Consoles.Interfaces;

#region Using Directives

using Forgotten_OOP.Entities;
using Forgotten_OOP.Entities.Interfaces;
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

#endregion



public class TeleportVial : Item, IStorable<Room>
{
    #region Private Fields

     /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();
    #endregion

    #region Constructor
    public TeleportVial(string name, string description, float weight) : base(name, description, weight)
    {
        name = "Fiala del Teletrasporto";
        description = "Una pozione che trasporta istantaneamente chi la beve in una stanza casuale del dungeon. Se un compagno si trova con te, ti seguirà automaticamente";
        weight = 4.0f;
    }
    #endregion

    #region Public Methods
    void IGrabbable.Grab(GameManager game)
    {
        game.Player.Backpack.Push(this);
        game.GameLogger.Log($"{Name} è stato aggiunto allo zaino.");
        GameConsole.WriteLine($"Hai raccolto: {Name}"); ;
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
                    if (!teleportTo.IsPinkRoom && !teleportTo.Equals(game.Player.CurrentRoom) && !teleportTo.Equals(enemy.CurrentRoom))
                    {
                        game.Player.Teleport(teleportTo);
                        check = false;
                    }
                }
            }
        });
        game.Player.Teleport(game.GameMap.GetRandomRoom());
        GameLogger.Log("Player used Teleport Vial");
        GameLogger.Log("Player teleported to room " + game.Player.CurrentRoom);
        game.IncrementActionsCount();
        GameConsole.WriteLine("Chiudi gli occhi per un secondo, senti un soffio di vento. Quando li riapri, ti trovi in una stanza diversa");
    }
    #endregion
}