namespace Forgotten_OOP.Entities;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// Represents Marlo, a special entity in the Forgotten OOP game that can teleport back to his initial room
/// </summary>
public class Marlo : Entity, IMarlo<Room>, IConsolable, ILoggable
{
    #region Private Fields

    /// <summary>
    /// Represents the initial room where Marlo starts
    /// </summary>
    private readonly Room initialRoom;

    #endregion

    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    #region Constructors

    /// <summary>
    /// Represents Marlo, a special entity in the Forgotten OOP game that can teleport back to his initial room
    /// </summary>
    public Marlo(Room startingRoom, Map<Room> gameMap) : base("Marlo", startingRoom, gameMap)
    {
        startingRoom.IsClosed = true;

        initialRoom = startingRoom;

        gameMap.Rooms = [startingRoom];
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void ReturnToInitialRoom()
    {
        GameLogger.Log($"{Name} is returning to the initial room {initialRoom}");

        Teleport(initialRoom);
    }

    #endregion
}
