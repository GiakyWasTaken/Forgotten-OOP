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
    private Configs configs = new();

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

        //configs = ReadConfigs();

        GameConsole.WriteLine("Welcome to Forgotten OOP!");
        GameConsole.WriteLine("1. Play");
        GameConsole.WriteLine("2. Settings");
        GameConsole.WriteLine("3. Exit");
        GameConsole.WriteLine("Please select an option: ");

        string choice = GameConsole.ReadLine();

        switch (choice)
        {
            case "1":
                Play();
                break;
            case "2":
                Settings();
                break;
            default:
                Exit();
                break;
        }
    }

    /// <inheritdoc />
    public void Play()
    {
        GameManager game = new(configs);

        GameLogger.Log("Game Started");

        game.StartGameLoop();
    }

    /// <inheritdoc />
    public void Settings()
    {
        GameConsole.WriteLine("Settings Menu");
        SettingsMenu settingsMenu = new();
        settingsMenu.Show();

        // WriteConfigs(configs);

        throw new NotImplementedException();
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