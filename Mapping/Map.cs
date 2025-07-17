namespace Forgotten_OOP.Mapping;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Enums;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents a map in the Forgotten OOP game
/// </summary>
public class Map<TRoom>(int mapDimension = 6) : IMap<TRoom>, IPrintableMap<TRoom>, IConsolable where TRoom : IRoom<TRoom>
{
    #region Properties

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public TRoom?[,] Layout { get; set; } = new TRoom[mapDimension, mapDimension];

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public bool TryGetRoom(int x, int y, out TRoom? room)
    {
        if (x < 0 || x >= mapDimension || y < 0 || y >= mapDimension)
        {
            room = default;
            return false;
        }

        room = Layout[x, y];

        return room != null;
    }

    /// <inheritdoc />
    public bool TryGetRoomInDirection(TRoom startingRoom, Direction direction, out TRoom? room)
    {
        (int x, int y) = GetRoomCoordinates(startingRoom);

        if (x < 0 || y < 0)
        {
            room = default;
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
                room = default;
                return false;
        }

        return room != null;
    }

    /// <inheritdoc />
    public TRoom GetRandomRoom()
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
    public Tuple<int, int> GetRoomCoordinates(TRoom room)
    {
        for (int i = 0; i < mapDimension; i++)
        {
            for (int j = 0; j < mapDimension; j++)
            {
                // Todo: Use a more robust comparison if necessary
                if (Layout[i, j]?.GetHashCode() == room.GetHashCode())
                {
                    return new Tuple<int, int>(i, j);
                }
            }
        }

        throw new ArgumentException("Room not found in the map layout.");
    }

    /// <inheritdoc />
    public void PrintMap()
    {
        GameConsole.Clear();
        GameConsole.WriteLine("Map Layout");
    }

    #endregion
}