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

        Player = new Player("Hero", GameMap.Layout[0, 0]!, GameMap, 3);

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
                Dictionary<Direction, Room?> adjRooms = enemy.CurrentRoom.GetAdjacentRooms();

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
    /// Initializes the game map
    /// </summary>
    /// <returns>An <see cref="Map{Room}"/> object</returns>
    private Map<Room> InitializeMap()
    {
        Map<Room> map = new();

        for (int i = 0; i < map.Layout.GetLength(0); i++)
        {
            for (int j = 0; j < map.Layout.GetLength(1); j++)
            {
                // Create a new room with a unique ID and add it to the map layout
                map.Layout[i, j] = new Room((i * map.Layout.GetLength(1)) + j, map);
            }
        }

        // Set the starting room
        map.Layout[0, 0] = new Room(0, map, isStartingRoom: true);

        // Set the enemy spawning room
        map.Layout[0, 1] = new Room(CreateRoomIdFromCoordinates(0, 1), map, isEnemySpawningRoom: false);
        map.Layout[1, 0] = new Room(CreateRoomIdFromCoordinates(1, 0), map, isEnemySpawningRoom: false);
        map.Layout[1, 1] = new Room(CreateRoomIdFromCoordinates(1, 1), map, isEnemySpawningRoom: false);

        return map;
    }

    /// <summary>
    /// Spawns items in the game world
    /// </summary>
    private void SpawnItems(Map<Room> map)
    {
        // Spawn keys in the map
        int keysSpawned = 0;

        do
        {
            Room keySpawnRoom = map.GetRandomRoom();

            if (keySpawnRoom.IsStartingRoom || keySpawnRoom.IsEnemySpawningRoom || !keySpawnRoom.IsPinkRoom)
            {
                continue;
            }

            // Todo: Use key class
            Item key = new("Key", "A key to unlock doors", 0);

            keySpawnRoom.ItemsOnGround.Push(key);

            keysSpawned++;

        } while (keysSpawned >= GameConfigs.NumKeys);
    }

    /// <summary>
    /// Spawns entities in the game world
    /// </summary>
    private void SpawnEntities(Map<Room> map)
    {
        // Spawn enemies in the map
        int enemiesSpawned = 0;

        do
        {
            Room enemySpawnRoom = map.GetRandomRoom();

            if (!enemySpawnRoom.IsEnemySpawningRoom)
            {
                continue;
            }

            Enemy enemy = new("Minotaur", enemySpawnRoom, map, GameConfigs.EnemyDelay);

            Entities.Add(enemy);

            enemiesSpawned++;

        } while (enemiesSpawned >= 1);
    }

    /// <summary>
    /// Calculates a unique room identifier based on the given coordinates
    /// </summary>
    /// <param name="x">The x-coordinate of the room within the game map</param>
    /// <param name="y">The y-coordinate of the room within the game map</param>
    /// <returns>An integer representing the unique room identifier</returns>
    private int CreateRoomIdFromCoordinates(int x, int y)
    {
        return x * GameMap.Layout.GetLength(1) + y;
    }

    #endregion
}