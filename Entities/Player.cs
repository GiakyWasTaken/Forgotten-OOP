namespace Forgotten_OOP.Entities;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;
#endregion

/// <summary>
/// Represents a player in the Forgotten OOP game
/// </summary>
public class Player(string name, Room startingRoom, Map<Room> gameMap, int lives) : Entity(name, startingRoom, gameMap), IPlayer<Room>, IFollowable, IConsolable, ILoggable
{
    #region Private Fields

    /// <summary>
    /// Represents the current room the player is in
    /// </summary>
    private Room currentRoom = startingRoom;

    #endregion

    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public int Lives { get; set; } = lives;

    /// <inheritdoc />
    public List<IKeyItem> KeyItems { get; } = [];

    /// <inheritdoc />
    public Stack<IStorable<Room>> Backpack { get; } = new();

    /// <inheritdoc />
    public List<Entity> FollowingEntities { get; set; } = [];

    /// <inheritdoc />
    public override Room CurrentRoom
    {
        get => currentRoom;
        protected set
        {
            if (currentRoom.Equals(value))
            {
                return;
            }

            currentRoom = value;

            foreach (Entity entity in FollowingEntities)
            {
                entity.Move(value);

                GameLogger.Log($"{entity.Name} followed the player moving to {CurrentRoom}");
            }
        }
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public float GetCurrentWeight()
    {
        return Backpack.Sum(item => item.Weight);
    }

    #endregion
}