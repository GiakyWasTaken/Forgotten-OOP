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
            GameConsole.WriteLine("3. Exit");

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
        GameManager game = new(configs);

        GameLogger.Log("Game Started");

        game.StartGameLoop();
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