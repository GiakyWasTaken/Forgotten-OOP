namespace Forgotten_OOP.GameManagers;

#region Using Directives

using System;
using System.Collections.Generic;

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Entities;
using Forgotten_OOP.Enums;
using Forgotten_OOP.GameManagers.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;

#endregion

/// <summary>
/// The main class for the Forgotten OOP game
/// </summary>
public class GameManager : IGameManager<Player, Entity, Map<Room>, Room>, IConsolable, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public Configs GameConfigs { get; }

    /// <inheritdoc />
    public Player Player { get; }

    /// <inheritdoc />
    public List<Entity> Entities { get; } = [];

    /// <inheritdoc />
    public Map<Room> GameMap { get; }

    /// <inheritdoc />
    public long ActionsCount { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="GameManager"/> class with the specified game configurations
    /// </summary>
    /// <param name="gameConfigs">A <see cref="GameConfigs"/> object containing the game configurations</param>
    public GameManager(Configs gameConfigs)
    {
        GameConfigs = gameConfigs;

        GameMap = InitializeMap();

        Player = new Player("Hero", GameMap.StartingRoom, GameMap, 3);

        SpawnItems(GameMap);

        SpawnEntities(GameMap);
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void StartGameLoop()
    {

    }

    /// <inheritdoc />
    public void IncrementActionsCount()
    {
        ActionsCount++;
        GameLogger.Log($"Action count incremented: {ActionsCount}");

        Entities.ForEach(entity =>
        {
            if (entity is Enemy enemy && ActionsCount % enemy.ActionDelay == 0)
            {
                Dictionary<Direction, Room> adjRooms = enemy.CurrentRoom.GetAdjacentRooms();

                List<Room> availableRooms = [];

                foreach (Direction direction in adjRooms.Keys)
                {
                    if (adjRooms[direction] is { IsPinkRoom: false } room)
                    {
                        availableRooms.Add(room);
                    }
                }

                if (availableRooms.Count > 0)
                {
                    Room nextRoom = availableRooms[Random.Shared.Next(availableRooms.Count)];

                    enemy.Move(nextRoom);

                    GameLogger.Log($"{enemy.Name} moved to room {nextRoom.GetCoordinates()}");
                }
            }
        });
    }

    /// <inheritdoc />
    public void SaveGame()
    {
        throw new NotImplementedException("Save game logic is not implemented yet.");
    }

    /// <inheritdoc />
    public void LoadGame()
    {
        throw new NotImplementedException("Load game logic is not implemented yet.");
    }

    /// <inheritdoc />
    public void EndGame()
    {
        throw new NotImplementedException("End game logic is not implemented yet.");
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Initializes and returns a new map with a predefined layout of rooms
    /// </summary>
    /// <returns>A <see cref="Map{Room}"/> object containing the initialized layout of rooms</returns>
    private Map<Room> InitializeMap()
    {
        int maxCoordinates = GameConfigs.MapDimension - 1;

        Map<Room> map = new(GameConfigs.MapDimension);

        // Initialize the map layout with rooms
        for (int y = 0; y < Math.Ceiling(GameConfigs.MapDimension / 2.0f); y++)
        {
            for (int x = 0; x < Math.Ceiling(GameConfigs.MapDimension / 2.0f); x++)
            {
                // Skip angles and some center rooms
                if ((x == y || Math.Abs(x - maxCoordinates) == y) && x % 2 == 0)
                {
                    continue;
                }

                PlaceMirrored(x, y);
                PlaceMirrored(maxCoordinates - x, y);
                PlaceMirrored(x, maxCoordinates - y);
                PlaceMirrored(maxCoordinates - x, maxCoordinates - y);
                continue;

                // Create and place all 4 mirrored rooms
                void PlaceMirrored(int mx, int my) =>
                    map.Layout[mx, my] ??= new Room(GenRoomId(mx, my), map,
                        isPinkRoom: x == 0 || x == maxCoordinates || y == 0 || y == maxCoordinates);
            }
        }

        map.Layout[maxCoordinates - 1, maxCoordinates] = new Room(GenRoomId(maxCoordinates - 1, maxCoordinates), map, isStartingRoom: true);

        // Set adjacent rooms to starting room not being enemy spawning rooms
        foreach (Room adjRoomToSpawn in map.StartingRoom.GetAdjacentRooms().Values.Where(room => room.IsEnemySpawningRoom))
        {
            adjRoomToSpawn.IsEnemySpawningRoom = false;

            foreach (Room adjRoomToAdjRoom in adjRoomToSpawn.GetAdjacentRooms().Values.Where(adjRoomToAdjRoom => adjRoomToAdjRoom.IsEnemySpawningRoom))
            {
                adjRoomToAdjRoom.IsEnemySpawningRoom = false;
            }
        }

        // Remove all pink rooms except for keys and npc spawning rooms
        map.Rooms
            .Where(room => room is { IsPinkRoom: true })
            .OrderBy(_ => Random.Shared.Next())
            .Skip(GameConfigs.NumKeys + 1)
            .ToList()
            .ForEach(room =>
            {
                Tuple<int, int> coordinates = map.GetRoomCoordinates(room);
                map.Layout[coordinates.Item1, coordinates.Item2] = null;
            });

        return map;

        int GenRoomId(int x, int y) => y * GameConfigs.MapDimension + x;
    }

    /// <summary>
    /// Spawns items in the game world
    /// </summary>
    private void SpawnItems(Map<Room> map)
    {
        // Spawn keys in the map
        List<Room> pinkRooms = [.. map.Rooms.Where(room => room is { IsPinkRoom: true })];

        for (int i = 0; i < GameConfigs.NumKeys; i++)
        {
            pinkRooms[i].ItemsOnGround.Push(new Key());
        }

        map.Rooms = pinkRooms;

        GameLogger.Log($"Spawned {GameConfigs.NumKeys} keys in the map.");
    }

    /// <summary>
    /// Spawns entities in the game world
    /// </summary>
    private void SpawnEntities(Map<Room> map)
    {
        // Spawn enemies in the map
        List<Room> spawningRooms = [.. map.Rooms.Where(room => room is { IsEnemySpawningRoom: true })];

        Entities.Add(new Enemy("Minotaur", spawningRooms[Random.Shared.Next(spawningRooms.Count)], map, GameConfigs.EnemyDelay));

        // Spawn good npc in the map in the last pink room
        List<Room> pinkRooms = [.. map.Rooms.Where(room => room is { IsPinkRoom: true })];

        Entities.Add(new Entity("Marlo", pinkRooms[^1], map));
    }

    #endregion
}