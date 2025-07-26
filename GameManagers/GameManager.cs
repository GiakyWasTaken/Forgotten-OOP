namespace Forgotten_OOP.GameManagers;

#region Using Directives

using System;
using System.Collections.Generic;
using System.Reflection;

using Forgotten_OOP.Commands.Interfaces;
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
    public bool IsGameRunning { get; private set; }

    /// <inheritdoc />
    public Configs GameConfigs { get; }

    /// <inheritdoc />
    public Player Player { get; }

    /// <inheritdoc />
    public List<Entity> Entities { get; }

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

        PopulateCommands();

        GameMap = InitializeMap();

        Player = new Player("Hero", GameMap.StartingRoom, GameMap, 3);

        SpawnItems(GameMap);

        Entities = SpawnEntities(GameMap);

        GameConsole.WriteLine("Ti trovi poco fuori il villaggio di Kuroka, hai trovato una grotta con un ingresso ad un dungeon di classe di classe S, uno tra i più pericolosi in assoluto.\n" +
            "Per questo motivo, l'entrata principale è stata sbarrata da tante travi di legno che sembravano essere state fissate in fretta e furia.\n" +
            "Nessuno di inesperto dovrebbe addentrarsi qui dentro, soprattutto tu, un cercatore di livello decisamente più basso rispetto a quello richiesto.\n" +
            "Ma non puoi tirarti indietro, tuo fratello è intrappolato lì, è l'unica persona che ti rimane e non vuoi perderlo.\n" +
            "Trovi un'entrata secondaria, per farti coraggio decidi di rileggere la lettera che di aiuto che Takumi ti ha mandato:\n\n" +
            "\"Fratello,\n" +
            "Spero che questa lettera ti raggiunga in tempo.\n" +
            "Sono ferito. C'è qualcosa qui… qualcosa che non dovrebbe esistere.\n" +
            "Si aggira tra queste stanze come se fosse casa sua.\n" +
            "Non provare ad affrontarlo. Non puoi.\n" +
            "Porta con te la Panacea. È l’unica cosa che può salvarmi.\n" +
            "Trovami. salvami. Fai in fretta.\n" +
            "Takumi.\"");
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void StartGameLoop()
    {
        bool? win;

        IsGameRunning = true;

        do
        {
            ICommand cmd = GameConsole.ReadCommand();

            cmd.Execute();

            ProcessEntities();

            win = CheckWinLoseCon();

        } while (IsGameRunning);

        if (win != null)
        {
            GameConsole.WriteLine(win == true
                ? "Congratulations! You have completed the game"
                : "Game Over! Better luck next time");
        }
    }

    /// <inheritdoc />
    public void IncrementActionsCount()
    {
        ActionsCount++;
        GameLogger.Log($"Action count incremented: {ActionsCount}");
    }

    /// <inheritdoc />
    public void EndGame()
    {
        IsGameRunning = false;
        GameLogger.Log("Game ended by user request.");
        GameConsole.WriteLine("Game ended. Thank you for playing!");
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
    /// Populates the list of available commands by instantiating all non-abstract classes that implement the <see
    /// cref="ICommand"/> interface within the current assembly
    /// </summary>
    private void PopulateCommands()
    {
        // Find all command classes that derive from BaseCommand
        List<Type> commandTypes = [.. Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && typeof(ICommand).IsAssignableFrom(t))];

        List<ICommand> commands = [];

        foreach (var type in commandTypes)
        {
            // Check if the command has a constructor that takes GameManager
            var constructorWithGameManager = type.GetConstructor([typeof(GameManager)]);
            if (constructorWithGameManager != null)
            {
                // Create an instance with this as parameter
                commands.Add((ICommand)constructorWithGameManager.Invoke([this]));
            }
            else
            {
                // Try to create with parameterless constructor
                var parameterlessConstructor = type.GetConstructor(Type.EmptyTypes);
                if (parameterlessConstructor != null)
                {
                    commands.Add((ICommand)parameterlessConstructor.Invoke([]));
                }
            }
        }

        GameConsole.Commands = commands;
        GameLogger.Log($"Populated {commands.Count} commands automatically.");
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
    private List<Entity> SpawnEntities(Map<Room> map)
    {
        List<Entity> entities = [];

        // Spawn enemies in the map
        List<Room> spawningRooms = [.. map.Rooms.Where(room => room is { IsEnemySpawningRoom: true })];

        entities.Add(new Enemy("Minotaur", spawningRooms[Random.Shared.Next(spawningRooms.Count)], map, GameConfigs.EnemyDelay));

        // Spawn good npc in the map in the last pink room
        List<Room> pinkRooms = [.. map.Rooms.Where(room => room is { IsPinkRoom: true })];

        entities.Add(new Entity("Marlo", pinkRooms[^1], map));

        return entities;
    }

    /// <summary>
    /// Processes the entities in the game, such as enemies and NPCs
    /// </summary>
    private void ProcessEntities()
    {
        Player.FollowingEntities.ForEach(entity =>
        {
            entity.Move(Player.CurrentRoom);
        });

        // Move enemies in the game world based on their action delay
        Entities.Where(entity => entity is Enemy enemy && ActionsCount % enemy.ActionDelay == 0)
            .ToList()
            .ForEach(enemy =>
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
            });
    }

    /// <summary>
    /// Determines the win or lose condition for the player in the game
    /// </summary>
    /// <returns><see langword="true"/> if the player has won; <see langword="false"/> if the player has lost; otherwise, <see
    /// langword="null"/> if the game is still ongoing</returns>
    private bool? CheckWinLoseCon()
    {
        // Check if the player has won or lost the game
        if (Player.CurrentRoom.IsStartingRoom && Player.FollowingEntities.Any(entity => entity.Name == "Marlo"))
        {
            return true;
        }

        if (Player.Lives <= 0)
        {
            return false;
        }

        return null;
    }

    #endregion
}