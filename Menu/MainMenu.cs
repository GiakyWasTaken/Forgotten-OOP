namespace Forgotten_OOP.Menu;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Menu.Interfaces;

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
    private Configs configs;

    /// <summary>
    /// Represents the game manager instance that handles the game logic
    /// </summary>
    private GameManager? game;

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

        GameConsole.WriteLine("Welcome to Forgotten OOP!");

        SettingsMenu settingsMenu = new();
        configs = settingsMenu.ReadConfigs();

        do
        {
            GameConsole.WriteLine("Please select an option: ");
            GameConsole.WriteLine("1. Play");
            GameConsole.WriteLine("2. Settings");

            if (game != null)
            {
                GameConsole.WriteLine("3. Save");
            }

            //if (File.Exists(saveFilePath))
            //{
            //    GameConsole.WriteLine("4. Load");
            //}

            GameConsole.WriteLine("5. Exit");

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
                    // Save the game state
                    Save();

                    break;

                case "4":
                    // Load the game state if a save file exists
                    //if (File.Exists(saveFilePath))
                    //{
                    //    Load();
                    //}
                    //else
                    //{
                    //    GameConsole.WriteLine("No saved game found.");
                    //}

                    break;

                default:
                    // Exit the game
                    Exit();
                    return;
            }

        } while (true);
    }

    /// <inheritdoc />
    public void Play()
    {
        GameLogger.Log("Starting Game...");
        game ??= new GameManager(configs);

        game.StartGameLoop();
    }

    /// <inheritdoc />
    public void Save()
    {
    }

    /// <inheritdoc />
    public void Load()
    {
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