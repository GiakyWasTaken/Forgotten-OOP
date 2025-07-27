namespace Forgotten_OOP.Menu;

#region Using Directives

using System.Text.Json;

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Menu.Interfaces;
using Forgotten_OOP.Serialization;

#endregion

/// <summary>
/// The main menu of the Forgotten OOP game
/// </summary>
public class MainMenu : IMainMenu, IConsolable, ILoggable
{
    #region Private Fields

    /// <summary>
    /// Represents the game configurations
    /// </summary>
    private Configs configs = new();

    /// <summary>
    /// Represents the game manager instance that handles the game logic
    /// </summary>
    private GameManager? game;

    /// <summary>
    /// Represents the default file name used for saving data
    /// </summary>
    private const string SafeFileName = "forgotten_oop.sav";

    /// <summary>
    /// Represents the file path where the game state will be saved
    /// </summary>
    private readonly string saveFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SafeFileName);

    /// <summary>
    /// Represents the JSON serializer options used for saving and loading the game
    /// </summary>
    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        IncludeFields = true
    };

    #endregion

    #region Properties

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void Show()
    {
        GameLogger.InitializeLogger();

        GameConsole.WriteLine("Benvenuto a Forgotten OOP!");

        SettingsMenu settingsMenu = new();
        configs = settingsMenu.ReadConfigs();

        do
        {
            GameConsole.WriteLine("Seleziona una opzione: ");
            GameConsole.WriteLine("1. Gioca");
            GameConsole.WriteLine("2. Impostazioni");

            if (game != null)
            {
                GameConsole.WriteLine("4. Salva");
            }

            if (File.Exists(saveFilePath))
            {
                GameConsole.WriteLine("5. Carica");
            }

            GameConsole.WriteLine("3. Esci");

            string choice = GameConsole.ReadLine();

            switch (choice)
            {
                case "1":
                    // Play the game
                    configs = settingsMenu.Configs;

                    Play();

                    break;
                case "2":
                    // Show settings menu
                    settingsMenu.Show();

                    break;

                case "3":
                    // Exit the game
                    Exit();
                    break;

                case "4":
                    // Save the game state
                    Save();
                    // Load the game state if a save file exists
                    

                    break;

                case "5":
                    if (File.Exists(saveFilePath))
                    {
                        Load();
                    }
                    else
                    {
                        GameConsole.WriteLine("Nessun salvataggio trovato.");
                    }
                    break;

                default:
                    Console.WriteLine("Comando non valido");
                    break;
            }

        } while (true);
    }

    /// <inheritdoc />
    public void Play()
    {
        GameLogger.Log("Avviando partita...");
        game ??= new GameManager(configs);

        game.StartGameLoop();
    }

    /// <inheritdoc />
    public void Save()
    {
        if (game == null)
        {
            GameConsole.WriteLine("No game is currently running to save.");
            return;
        }

        try
        {
            string gameStateJson = GameStateConverter.SerializeGameState(game, jsonSerializerOptions);

            File.WriteAllText(saveFilePath, gameStateJson);
            GameLogger.Log($"Game saved to {saveFilePath}");
            GameConsole.WriteLine($"Game saved to {saveFilePath}");
        }
        catch (Exception ex)
        {
            GameLogger.Log($"Failed to save game: {ex.Message}");
            GameConsole.WriteLine("Failed to save game.");
        }
    }

    /// <inheritdoc />
    public void Load()
    {
        try
        {
            if (!File.Exists(saveFilePath))
            {
                GameConsole.WriteLine("Save file not found.");
                return;
            }

            string gameStateJson = File.ReadAllText(saveFilePath);
            var gameStateDto = GameStateConverter.DeserializeGameState(gameStateJson, jsonSerializerOptions);

            game = GameStateConverter.ConvertFromDto(gameStateDto);

            // Small check to ensure the game is equal to the saved one
            if (gameStateDto.GameMap.Rooms
                .Where(roomDto => roomDto.IsPinkRoom)
                .All(pinkRoomDto => game.GameMap.Rooms
                    .Where(room => room.IsPinkRoom)
                    .Any(r => r.Id == pinkRoomDto.Id)))
            {
                GameLogger.Log($"Game loaded from {saveFilePath}");
                GameConsole.WriteLine($"Game loaded from {saveFilePath}");
            }
            else
            {
                throw new InvalidOperationException("The loaded game state does not match the expected format");
            }
        }
        catch (Exception ex)
        {
            GameLogger.Log($"Failed to load game: {ex.Message}");
            GameConsole.WriteLine("Failed to load game.");
        }
    }

    /// <inheritdoc />
    public void Exit()
    {
        GameConsole.WriteLine("Thank you for playing Forgotten OOP!");

        GameLogger.Log("Game Exited");

        Environment.Exit(0);
    }

    #endregion
}