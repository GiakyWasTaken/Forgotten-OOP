namespace Forgotten_OOP.GameManagers;

#region Using Directives

using Forgotten_OOP.GameManagers.Interfaces;
using Forgotten_OOP.Injectables;
using Forgotten_OOP.Injectables.Interfaces;
using System.IO;
using System.Text.Json;
#endregion

/// <summary>
/// The main menu of the Forgotten OOP game
/// </summary>
public class MainMenu : IMainMenu, IConfigurable, IConsolable, ILoggable
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

        configs = ReadConfigs();

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

        WriteConfigs(configs);

        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Exit()
    {
        GameConsole.WriteLine("Thank you for playing Forgotten OOP!");

        GameLogger.Log("Game Exited");

        Environment.Exit(0);
    }

    // <inheritdoc />
    public Configs ReadConfigs()
    {
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs.json");    //obtains "configs.json" path

        if (File.Exists(configPath))
        {
                string json = File.ReadAllText(configPath);
                Configs loaded = JsonSerializer.Deserialize<Configs>(json);
                return loaded;
        }
        else
        {
            GameLogger.Log("Configuration file not found. Using deafult values");
            return new Configs();
        }
    }

    /// <inheritdoc />
    public void WriteConfigs(Configs configs)
    {
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs.json");
            // creation of .json file
            string json = JsonSerializer.Serialize(configs, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(configPath, json);
            GameLogger.Log("Configurazione salvata con successo.");
    }

    #endregion
}