namespace Forgotten_OOP.Mapping;

#region Using Directives

using System.Text;

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Enums;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents a map in the Forgotten OOP game
/// </summary>
public class Map<TRoom>(int mapDimension) : IMap<TRoom>, IPrintableMap<TRoom>, IConsolable where TRoom : IRoom<TRoom>
{
    #region Private Fields

    /// <summary>
    /// Represents the dimension of the map (number of rooms along one side)
    /// </summary>
    private readonly int mapDimension = mapDimension;

    #endregion

    #region Properties

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public TRoom?[,] Layout { get; set; } = new TRoom[mapDimension, mapDimension];

    /// <inheritdoc />
    public List<TRoom> Rooms
    {
        get
        {
            List<TRoom> rooms = [];

            for (int y = 0; y < mapDimension; y++)
            {
                for (int x = 0; x < mapDimension; x++)
                {
                    TRoom? room = Layout[x, y];
                    if (room != null)
                    {
                        rooms.Add(room);
                    }
                }
            }

            return rooms;
        }
        set
        {
            foreach (TRoom room in value)
            {
                if (room == null)
                {
                    throw new ArgumentNullException(nameof(value), "Room cannot be null.");
                }

                try
                {
                    Tuple<int, int> coordinates = GetRoomCoordinates(room);
                    Layout[coordinates.Item1, coordinates.Item2] = room;
                }
                catch (KeyNotFoundException)
                {
                    throw new ArgumentException($"Room {room} does not exist in the map layout.", nameof(value));
                }
            }
        }
    }

    /// <summary>
    /// Gets the starting room from the map layout
    /// </summary>
    public TRoom StartingRoom
    {
        get
        {
            return Rooms
                .FirstOrDefault(room => room.IsStartingRoom)
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
            case Direction.West:
                x--;
                break;
            case Direction.South:
                y++;
                break;
            case Direction.East:
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
        int x = Random.Shared.Next(mapDimension);
        int y = Random.Shared.Next(mapDimension);

        while (Layout[x, y] == null)
        {
            x = Random.Shared.Next(mapDimension);
            y = Random.Shared.Next(mapDimension);
        }

        return Layout[x, y]!;
    }

    /// <inheritdoc />
    public TRoom GetRandomRoom(Func<TRoom, bool> predicate)
    {
        List<TRoom> filteredRooms = [.. Rooms.Where(predicate)];

        if (filteredRooms.Count == 0)
        {
            throw new InvalidOperationException("No room satisfies the given condition.");
        }

        return filteredRooms[Random.Shared.Next(filteredRooms.Count)];
    }

    /// <inheritdoc />
    public Tuple<int, int> GetRoomCoordinates(TRoom room)
    {
        for (int y = 0; y < mapDimension; y++)
        {
            for (int x = 0; x < mapDimension; x++)
            {
                if (room.Equals(Layout[x, y]))
                {
                    return new Tuple<int, int>(x, y);
                }
            }
        }

        throw new KeyNotFoundException($"Room {room} not found in the map layout.");
    }

    /// <inheritdoc />
    public void PrintMap(List<IEntity<TRoom>>? entities = null, bool showPlayer = true, bool showEnemy = false, bool showKey = false, bool showMarlo = false, bool showStartingRoom = false, bool showRooms = false)
    {
        var mapBuilder = new StringBuilder();
        mapBuilder.AppendLine("Layout della mappa - Tu sei [P]");

        for (int y = 0; y < mapDimension; y++)
        {
            // Print top border of cells
            for (int x = 0; x < mapDimension; x++)
            {
                mapBuilder.Append("+---");
            }

            mapBuilder.AppendLine("+");

            // Print cell contents
            for (int x = 0; x < mapDimension; x++)
            {
                mapBuilder.Append('|');

                TRoom? room = Layout[x, y];

                // Determine the character to represent the room
                string roomChar = room switch
                {
                    null => "   ",
                    _ when showKey && room.ItemsOnGround.OfType<IKeyItem>().Any() => "[K]",
                    _ when showEnemy && entities?.OfType<IEnemy<TRoom>>().Any(enemy => enemy.CurrentRoom.Equals(room)) == true => "[M]",
                    _ when showPlayer && entities?.OfType<IPlayer<TRoom>>().Any(player => player.CurrentRoom.Equals(room)) == true => "[P]",
                    _ when showMarlo && entities?.OfType<IMarlo<TRoom>>().Any(marlo => marlo.CurrentRoom.Equals(room)) == true => "[N]",
                    _ when showStartingRoom && room.IsStartingRoom => "[S]",
                    _ => showRooms ? "[ ]" : "   "
                };

                mapBuilder.Append(roomChar);
            }

            mapBuilder.AppendLine("|");
        }

        // Print bottom border
        for (int y = 0; y < mapDimension; y++)
        {
            mapBuilder.Append("+---");
        }

        mapBuilder.AppendLine("+");

        // Output the entire map at once
        GameConsole.WriteLine(mapBuilder.ToString(), skipWriteAnimation: true);
    }

    #endregion
}