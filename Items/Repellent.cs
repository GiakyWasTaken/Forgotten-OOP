using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Mapping;
using Forgotten_OOP.Entities;
using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;

namespace Forgotten_OOP.Items
{
    internal class Repellent : Item, IStorable<Room>
    {
        /// <inheritdoc />
        public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

        /// <inheritdoc />
        public IConsole GameConsole => ServiceHelper.GetService<IConsole>();
        public Repellent(string name, string description, float weight) : base(name, description, weight)
        {
            name = "Scacciapresenze";
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
                    GameLogger.Log("Player used Repellent");
                    GameLogger.Log($"{enemy.Name} moved to room {enemy.CurrentRoom}");
                    game.IncrementActionsCount();
                }
            });
            GameConsole.WriteLine("pop the baby"); //TODO: insert line 
        }
    }
}
