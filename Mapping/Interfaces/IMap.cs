namespace Forgotten_OOP.Mapping.Interfaces;

#region Using Directives

using Forgotten_OOP.Enums;

#endregion

/// <summary>
/// An interface for a map
/// </summary>
public interface IMap<TRoom> where TRoom : IRoom<TRoom>
{
    /// <summary>
    /// Represents the layout of the map as a 2D array of rooms
    /// </summary>
    public TRoom?[,] Layout { get; set; }

    /// <summary>
    /// Gets the collection of rooms available in the current context
    /// </summary>
    public List<TRoom> Rooms { get; }

    /// <summary>
    /// Attempts to retrieve a room at the specified coordinates
    /// </summary>
    /// <param name="x">The x-coordinate of the room to retrieve</param>
    /// <param name="y">The y-coordinate of the room to retrieve</param>
    /// <param name="room">When this method returns, contains the room at the specified coordinates, if found; otherwise, <see
    /// langword="null"/></param>
    /// <returns><see langword="true"/> if a room exists at the specified coordinates; otherwise, <see langword="false"/></returns>
    public bool TryGetRoom(int x, int y, out TRoom? room);

    /// <summary>
    /// Attempts to retrieve the room located in the specified direction from the given coordinates
    /// </summary>
    /// <param name="x">The x-coordinate of the starting room</param>
    /// <param name="y">The y-coordinate of the starting room</param>
    /// <param name="direction">The direction in which to look for the adjacent room</param>
    /// <param name="room">When this method returns, contains the room found in the specified direction, if any; otherwise, <see
    /// langword="null"/></param>
    /// <returns><see langword="true"/> if a room is found in the specified direction; otherwise, <see langword="false"/></returns>
    public bool TryGetRoomInDirection(int x, int y, Direction direction, out TRoom? room);

    /// <summary>
    /// Attempts to retrieve the room located in the specified direction from the starting room
    /// </summary>
    /// <param name="startingRoom">The room from which to start the search</param>
    /// <param name="direction">The direction in which to look for the adjacent room</param>
    /// <param name="room">When this method returns, contains the room found in the specified direction, if any; otherwise, <see
    /// langword="null"/></param>
    /// <returns><see langword="true"/> if a room is found in the specified direction; otherwise, <see langword="false"/></returns>
    public bool TryGetRoomInDirection(TRoom startingRoom, Direction direction, out TRoom? room);

    /// <summary>
    /// Retrieves a random room from the collection of available rooms
    /// </summary>
    /// <returns>A randomly selected room of type <typeparamref name="TRoom"/> from the available rooms Returns <see
    /// langword="null"/> if no rooms are available</returns>
    public TRoom GetRandomRoom();

    /// <summary>
    /// Retrieves a random room that satisfies a given condition specified by a predicate.
    /// </summary>
    /// <param name="predicate">A function that defines the condition the room must satisfy.</param>
    /// <returns>A randomly selected room of type <typeparamref name="TRoom"/> that satisfies the condition</returns>
    /// <exception cref="InvalidOperationException">Thrown if no room satisfies the given condition.</exception>
    public TRoom GetRandomRoom(Func<TRoom, bool> predicate);

    /// <summary>
    /// Retrieves the coordinates of the specified room
    /// </summary>
    /// <param name="room">The room for which to obtain coordinates Cannot be null</param>
    /// <returns>A <see cref="Tuple{T1, T2}"/> containing the X and Y coordinates of the room</returns>
    public Tuple<int, int> GetRoomCoordinates(TRoom room);
}

