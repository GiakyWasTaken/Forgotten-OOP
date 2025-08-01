namespace Forgotten_OOP.Mapping;

#region Using Directives

using Forgotten_OOP.Enums;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents a room in the Forgotten OOP game
/// </summary>
public class Room(long id, Map<Room> gameMap, bool isStartingRoom = false, bool isEnemySpawningRoom = true, bool isPinkRoom = false, bool isInitiallyClosed = false) : IRoom<Room>
{
    #region Private Fields

    /// <summary>
    /// Reference to the game map for spatial queries
    /// </summary>
    private readonly Map<Room>? gameMap = gameMap;

    #endregion

    #region Properties

    /// <inheritdoc />
    public long Id { get; } = id;

    /// <inheritdoc />
    public List<IItem> ItemsOnGround { get; } = [];

    /// <inheritdoc />
    public bool IsStartingRoom { get; } = isStartingRoom;

    /// <inheritdoc />
    public bool IsEnemySpawningRoom { get; set; } = isEnemySpawningRoom && !isPinkRoom;

    /// <inheritdoc />
    public bool IsPinkRoom { get; } = isPinkRoom;

    /// <inheritdoc />
    public bool IsClosed { get; set; } = isInitiallyClosed;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public Tuple<int, int>? GetCoordinates()
    {
        if (gameMap == null)
        {
            return null;
        }

        try
        {
            return gameMap.GetRoomCoordinates(this);
        }
        catch (ArgumentException)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public Dictionary<Direction, Room> GetAdjacentRooms()
    {
        Dictionary<Direction, Room> adjacentRooms = [];

        if (gameMap == null)
        {
            return adjacentRooms;
        }

        foreach (Direction direction in Enum.GetValues<Direction>())
        {
            if (gameMap.TryGetRoomInDirection(this, direction, out Room? room) && room != null)
            {
                adjacentRooms[direction] = room;
            }
        }

        return adjacentRooms;
    }

    /// <inheritdoc />
    public bool Equals(Room? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id == other.Id && GetCoordinates()?.Equals(other.GetCoordinates()) == true;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return Equals(obj as Room);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, GetCoordinates());
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"Room with coordinates {GetCoordinates()}";
    }

    #endregion
}
