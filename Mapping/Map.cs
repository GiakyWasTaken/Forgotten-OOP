namespace Forgotten_OOP.Mapping;

#region Using Directives

using System.Text;

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Enums;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents a map in the Forgotten OOP game
/// </summary>
public class Map<TRoom>(int mapDimension) : IMap<TRoom>, IPrintableMap<TRoom>, IConsolable where TRoom : IRoom<TRoom>
{
    #region Properties

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public TRoom?[,] Layout { get; set; } = new TRoom[mapDimension, mapDimension];

    /// <summary>
    /// Gets the starting room from the map layout
    /// </summary>
    public TRoom StartingRoom
    {
        get
        {
            return Layout.Cast<TRoom?>()
                .FirstOrDefault(room => room?.IsStartingRoom == true)
                ?? throw new KeyNotFoundException("No starting room found in the map layout.");
        }
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public bool TryGetRoom(int x, int y, out TRoom? room)
    {
        room = default;

        try
        {
            TRoom? retRoom = Layout[x, y];

            if (retRoom == null)
            {
                return false;
            }

            room = retRoom;
            return true;
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }

    /// <inheritdoc />
    public bool TryGetRoomInDirection(int x, int y, Direction direction, out TRoom? room)
    {
        room = default;

        switch (direction)
        {
            case Direction.North:
                y--;
                break;
            case Direction.South:
                y++;
                break;
            case Direction.East:
                x--;
                break;
            case Direction.West:
                x++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        return TryGetRoom(x, y, out room);
    }

    /// <inheritdoc />
    public bool TryGetRoomInDirection(TRoom startingRoom, Direction direction, out TRoom? room)
    {
        try
        {
            (int x, int y) = GetRoomCoordinates(startingRoom);
            return TryGetRoomInDirection(x, y, direction, out room);
        }
        catch (KeyNotFoundException)
        {
            room = default;
            return false;
        }
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

        throw new KeyNotFoundException($"Room {room} not found in the map layout.");
    }

    /// <inheritdoc />
    public void PrintMap()
    {
        var mapBuilder = new StringBuilder();
        mapBuilder.AppendLine("Map Layout");

        for (int i = 0; i < Layout.GetLength(0); i++)
        {
            // Print top border of cells
            for (int j = 0; j < Layout.GetLength(1); j++)
            {
                mapBuilder.Append("+---");
            }

            mapBuilder.AppendLine("+");

            // Print cell contents
            for (int j = 0; j < Layout.GetLength(1); j++)
            {
                mapBuilder.Append('|');

                TRoom? room = Layout[i, j];

                // Determine the character to represent the room
                string roomChar = room switch
                {
                    null => "   ",
                    _ when room.IsStartingRoom => " S ",
                    _ when room.IsEnemySpawningRoom => " E ",
                    _ when room.IsPinkRoom => " P ",
                    _ => " N "
                };

                mapBuilder.Append(roomChar);
            }

            mapBuilder.AppendLine("|");
        }

        // Print bottom border
        for (int j = 0; j < Layout.GetLength(1); j++)
        {
            mapBuilder.Append("+---");
        }

        mapBuilder.AppendLine("+");

        // Output the entire map at once
        GameConsole.Clear();
        GameConsole.WriteLine(mapBuilder.ToString());
    }

    #endregion
}