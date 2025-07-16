namespace Forgotten_OOP.GameManagers;

using System.Numerics;
using System;
using System.Reflection.Metadata;

#region Using Directives

using Forgotten_OOP.GameManagers.Interfaces;
using Forgotten_OOP.Injectables;
using Forgotten_OOP.Injectables.Interfaces;
using Forgotten_OOP.Mapping;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

#endregion

/// <summary>
/// The main class for the Forgotten OOP game
/// </summary>
public class GameManager : IGameManager, IConsolable, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public Configs GameConfigs { get; }

    /// <inheritdoc />
    public Map<Room> GameMap { get; }

    /// <inheritdoc />
    public long ActionsCount { get; } = 0;

    /// <inheritdoc />
    public Stack<IStorable> BackPack { get; }; //TODO: define IStorable

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="GameManager"/> class with the specified game configurations
    /// </summary>
    /// <param name="gameConfigs">A <see cref="GameConfigs"/> object containing the game configurations</param>
    public GameManager(Configs gameConfigs)
    {
        GameConfigs = gameConfigs;

        //GameMap = InitializeMap();

        //SpawnItems();

        //SpawnEntities();
    }

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void StartGameLoop()
    {
        Console.Clear();
        //throw new NotImplementedException("Start game loop logic is not implemented yet.");
        Console.WriteLine("What will you do now?");
        Console.WriteLine("1. Interact");
        Console.WriteLine("2. Move");
        Console.WriteLine("3. Open bag");
        Console.Write("Please select an option: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                //Add Item to bag / start event
                
                break;
            case "2":
                //Move
                
                break;
            case "3":
                OpenBag();
                Program.GameLogger.Log("Player opened bag");
                break;
            
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
        
    }

    public void OpenBag()
    {
        Console.Clear();
        //Items pile
        Console.WriteLine("what do you want to do?");
        Console.WriteLine("1. Use item");
        Console.WriteLine("2. Drop item");
        Console.WriteLine("3. Close bag");
        Console.Write("Please select an option: ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                //depending on item, call relative function
                break;
            case "2":
                //item.drop
                break;
            case"3":
                StartGameLoop();
                Program.GameLogger.Log("Player closed bag");
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
     
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
    /// <returns>An <see cref="Map"/> object</returns>
    private Map<Room> InitializeMap()
    {
        return new Map<Room>();
    }

    /// <summary>
    /// Spawns items in the game world
    /// </summary>
    private void SpawnItems()
    {
        throw new NotImplementedException("Item spawning logic is not implemented yet.");
    }

    /// <summary>
    /// Spawns entities in the game world
    /// </summary>
    private void SpawnEntities()
    {
       /* public Player player = new Player();
        public Enemy enemyNPC = new Enemy();
        public Entity allyNPC = new Entity();

        List<Entity> entityList = new List<Entity>();
        entityList.Add(player);
        entityList.Add(enemyNPC);
        entityList.Add(allyNPC);*/
    }

    #endregion
}