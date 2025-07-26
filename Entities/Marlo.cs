namespace Forgotten_OOP.Entities;

#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;
#endregion

public class Marlo(string name, Room startingRoom, Map<Room> gameMap) : Entity(name, startingRoom, gameMap), IMarlo, IConsolable, ILoggable
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <summary>
    /// Represents the current room Marlo is in
    /// </summary>
    private Room currentRoom = startingRoom;

    #endregion
}
