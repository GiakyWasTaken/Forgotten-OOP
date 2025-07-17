using Forgotten_OOP.Consoles.Interfaces;
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

namespace Forgotten_OOP.Items
{
    internal class Bandage : Item, IStorable<Room>
    {
        /// <inheritdoc />
        public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

        /// <inheritdoc />
        public IConsole GameConsole => ServiceHelper.GetService<IConsole>();
        public Bandage(string name, string description, float weight) : base(name, description, weight)
        {
            name = "Bende";
            description = "Puoi usarle per curare le tue ferite";
            weight = 2.0f;
        }

        void IGrabbable.Grab()
        {
            throw new NotImplementedException();
        }

        void IItem.Use(GameManager game)
        {
            game.Player.Lives += 1;
            GameLogger.Log("Player used bandages");
            game.IncrementActionsCount();
        }
    }
}
