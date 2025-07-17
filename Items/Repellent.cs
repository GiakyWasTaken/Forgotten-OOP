using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Mapping;
using Forgotten_OOP.Entities;

namespace Forgotten_OOP.Items
{
    internal class Repellent : Item, IStorable<Room>
    {
        public Repellent(string name, string description, float weight) : base(name, description, weight)
        {
            name = "Repellente";
            description = "Utilizzarlo scaccia il mostro";
            weight = 4.0f;
        }

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
                    enemy.Teleport(game.GameMap.GetRandomRoom());
                    game.GameLogger.Log("Player used Repellent");
                    game.GameLogger.Log($"{enemy.Name} moved to room {enemy.CurrentRoom}");
                    game.IncrementActionsCount();
                }
            });
        }
    }
}
