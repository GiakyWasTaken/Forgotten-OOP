using Forgotten_OOP.Entities;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging;
using Forgotten_OOP.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forgotten_OOP.Items
{
    internal class TeleportVial : Item, IStorable<Room>
    {


        public TeleportVial(string name, string description, float weight) : base(name, description, weight)
        {
            name = "Fiala del Teletrasporto";
            description = "Berla ti teletrasporta in una stanza casuale";
            weight = 4.0f;
        }

        void IGrabbable.Grab()
        {
            throw new NotImplementedException();
        }

        void IItem.Use(GameManager game)
        {
            game.Player.Teleport(game.GameMap.GetRandomRoom());
            game.GameLogger.Log("Player used Teleport Vial");
            game.GameLogger.Log("Player teleported to room "+game.Player.CurrentRoom);

            game.IncrementActionsCount();
        }
    }
}
