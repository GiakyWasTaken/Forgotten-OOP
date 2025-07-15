namespace Forgotten_OOP.Mapping;

#region Using Directives

using Forgotten_OOP.Enums;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents a map in the Forgotten OOP game
/// </summary>
public class Map(int mapDimension = 6) : IMap<Room>
{
    #region Properties

    public Room?[,] Layout { get; set; } = new Room[mapDimension, mapDimension];

    /// <inheritdoc />
    public Room StartingRoom { get; set; }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public bool TryGetRoom(int x, int y, out Room? room)
    {
        if (x < 0 || x >= mapDimension || y < 0 || y >= mapDimension)
        {
            room = null;
            return false;
        }

        room = Layout[x, y];

        return room != null;
    }

    /// <inheritdoc />
    public bool TryGetRoomInDirection(Room startingRoom, Direction direction, out Room? room)
    {
        (int x, int y) = GetRoomCoordinates(startingRoom);

        if (x < 0 || y < 0)
        {
            room = null;
            return false;
        }

        room = Layout[x, y];

        if (room == null)
        {
            return false;
        }

        switch (direction)
        {
            case Direction.North:
                if (x > 0)
                {
                    room = Layout[x - 1, y];
                }

                break;
            case Direction.South:
                if (x < mapDimension - 1)
                {
                    room = Layout[x + 1, y];
                }

                break;
            case Direction.East:
                if (y < mapDimension - 1)
                {
                    room = Layout[x, y + 1];
                }

                break;
            case Direction.West:
                if (y > 0)
                {
                    room = Layout[x, y - 1];
                }

                break;
            default:
                room = null;
                return false;
        }

        return room != null;
    }

    /// <inheritdoc />
    public Room GetRandomRoom()
    {
        Random random = new();
        int x = random.Next(mapDimension);
        int y = random.Next(mapDimension);

        while (Layout[x, y] == null)
        {
            x = random.Next(mapDimension);
            y = random.Next(mapDimension);
        }

        return Layout[x, y]!;
    }

    /// <inheritdoc />
    public Tuple<int, int> GetRoomCoordinates(Room room)
    {
        for (int i = 0; i < mapDimension; i++)
        {
            for (int j = 0; j < mapDimension; j++)
            {
                if (Layout[i, j] == room)
                {
                    return new Tuple<int, int>(i, j);
                }
            }
        }

        throw new ArgumentException("Room not found in the map layout.");
    }

    #endregion
}