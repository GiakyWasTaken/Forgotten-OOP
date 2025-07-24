namespace Forgotten_OOP.Menu;

#region Using Directives

using System;
using System.Text.Json;

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.GameManagers.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Menu.Interfaces;

#endregion

/// <summary>
/// Provides a menu interface for configuring game settings, such as enemy delay, map dimensions, and item counts
/// </summary>
public class SettingsMenu : ISettingsMenu, IConfigurable, ILoggable, IConsolable
{
    #region Private Fields

    /// <summary>
    /// Represents the JSON serializer options used for reading and writing configurations
    /// </summary>
    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    /// <summary>
    /// Represents the configuration settings for the application
    /// </summary>
    private Configs configs = new();

    #endregion

    #region Properties

    /// <inheritdoc />
    public Configs Configs
    {
        get => configs;
        set => configs = value;
    }

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void Show()
    {
        Configs = ReadConfigs();

        while (true)
        {
            GameConsole.WriteLine("Cose vuoi fare?");
            GameConsole.WriteLine("1: Cambia Enemy Delay");
            GameConsole.WriteLine("2: Cambia Dimensione Mappa");
            GameConsole.WriteLine("3: Cambia Peso Massimo");
            GameConsole.WriteLine("4: Cambia Numero Oggetti");
            GameConsole.WriteLine("5: Cambia Numero Chiavi");
            GameConsole.WriteLine("6: Salva e torna al menu");
            GameConsole.WriteLine("_____________________");

            string choice = GameConsole.ReadLine();

            switch (choice)
            {
                case "1":
                    ChangeEnemyDelay();
                    break;
                case "2":
                    ChangeMapDimension();
                    break;
                case "3":
                    ChangeMaxWeight();
                    break;
                case "4":
                    ChangeNumItems();
                    break;
                case "5":
                    ChangeNumKeys();
                    break;
                case "6":
                    WriteConfigs(Configs);
                    return;
                default:
                    GameConsole.WriteLine("Scelta non riconosciuta, riprova");
                    break;
            }
        }
    }

    /// <inheritdoc />
    public void ChangeEnemyDelay()
    {
        GameConsole.WriteLine("Vuoi cambiare l'Enemy Delay? Default = 3, Attuale = " + configs.EnemyDelay);

        string input = GameConsole.ReadLine();

        if (int.TryParse(input, out int num))
        {
            configs.EnemyDelay = num;
        }
    }

    /// <inheritdoc />
    public void ChangeMapDimension()
    {
        GameConsole.WriteLine("Vuoi cambiare la dimensione della mappa? Default = 7, Attuale = " + configs.MapDimension);

        string input = GameConsole.ReadLine();

        if (int.TryParse(input, out int num))
        {
            configs.MapDimension = num;
        }
    }

    /// <inheritdoc />
    public void ChangeMaxWeight()
    {
        GameConsole.WriteLine("Vuoi cambiare il peso massimo trasportabile? Default = 10.0, Attuale = " + configs.MaxWeight);

        string input = GameConsole.ReadLine();

        if (float.TryParse(input, out float num))
        {
            configs.MaxWeight = num;
        }
    }

    /// <inheritdoc />
    public void ChangeNumItems()
    {
        GameConsole.WriteLine("Vuoi cambiare il numero di Item generati nelle stanze? Default = 10, Attuale = " + configs.NumItems);

        string input = GameConsole.ReadLine();

        if (int.TryParse(input, out int num))
        {
            configs.NumItems = num;
        }
    }

    /// <inheritdoc />
    public void ChangeNumKeys()
    {
        GameConsole.WriteLine("Vuoi cambiare il numero di chiavi generate nelle stanze? Default = 10, Attuale = " + configs.NumKeys);

        string input = GameConsole.ReadLine();

        if (int.TryParse(input, out int num))
        {
            configs.NumKeys = num;
        }
    }

    /// <inheritdoc />
    public Configs ReadConfigs()
    {
        // Obtains "configs.json" path
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs.json");

        Configs loaded = new();

        try
        {
            if (File.Exists(configPath))
            {
                string json = File.ReadAllText(configPath);

                loaded = JsonSerializer.Deserialize<Configs>(json, jsonSerializerOptions);
            }
            else
            {
                GameLogger.Log("Configuration file not found. Using default values");
            }
        }
        catch (Exception ex)
        {
            GameLogger.Log($"Error reading configuration file: {ex.Message}");
        }

        return loaded;
    }

    /// <inheritdoc />
    public void WriteConfigs(Configs newConfigs)
    {
        try
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs.json");

            // Creation of .json file
            string json = JsonSerializer.Serialize(newConfigs, jsonSerializerOptions);

            File.WriteAllText(configPath, json);

            GameLogger.Log("Configuration file written successfully");
        }
        catch (Exception ex)
        {
            GameLogger.Log($"Error writing configuration file: {ex.Message}");
        }
    }

    #endregion
}
