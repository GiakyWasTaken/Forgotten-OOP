using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;
using Forgotten_OOP.Entities;
using Forgotten_OOP.Enums;

namespace Forgotten_OOP.Items
{
    internal class GuardianEye : Item, IStorable<Room>
    {
        /// <inheritdoc />
        public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

        /// <inheritdoc />
        public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

        public GuardianEye(string name, string description, float weight) : base(name, description, weight)
        {
            name = "Occhio del Guardiano";
            description = "Ti dice dietro quale porta il mostro si sta nascondendo";
            weight = 6.0f;
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
                    Dictionary<Direction, Room?> ADJ =[];
                    ADJ=enemy.CurrentRoom.GetAdjacentRooms();
                    for (int i = 0; i < ADJ.Count; i++)
                    {
                        Direction dir = (Direction)i;
                        if (ADJ[dir].GetCoordinates() == game.Player.CurrentRoom.GetCoordinates())
                        {
                            GameConsole.WriteLine("(WIP) il mostro si trova a " + (Direction)(5 - i));
                        }
                    }
                }
            });
            GameLogger.Log("Player used Guardian's Eye");
            game.IncrementActionsCount();
        }
    }
}
