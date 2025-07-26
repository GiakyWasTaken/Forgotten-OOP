namespace Forgotten_OOP.Menu;

#region Using Directives

using System;
using System.Reflection;
using System.Text.Json;

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.GameManagers.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Menu.Interfaces;

using static System.Int32;

#endregion

/// <summary>
/// Provides a menu interface for configuring game settings, such as enemy delay, map dimensions, and item counts
/// </summary>
public class SettingsMenu : IMenu, IConfigurable, ILoggable, IConsolable
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

            bool tryParse = TryParse(GameConsole.ReadLine(), out int choice);

            switch (tryParse ? choice : -1)
            {
                case 1:
                    ChangeConfigValue("Vuoi cambiare l'Enemy Delay? Default = 3,", configs.EnemyDelay, value => configs.EnemyDelay = value);
                    break;
                case 2:
                    ChangeConfigValue("Vuoi cambiare la dimensione della mappa? Default = 7,", configs.MapDimension, value => configs.MapDimension = value);
                    break;
                case 3:
                    ChangeConfigValue("Vuoi cambiare il peso massimo trasportabile? Default = 10.0,", configs.MaxWeight, value => configs.MaxWeight = value);
                    break;
                case 4:
                    ShowItemMenu();
                    break;
                case 5:
                    ChangeConfigValue("Vuoi cambiare il numero di chiavi generate nelle stanze? Default = 1,", configs.NumKeys, value => configs.NumKeys = value);
                    break;
                case 6:
                    WriteConfigs(Configs);
                    return;
                default:
                    GameConsole.WriteLine("Scelta non riconosciuta, riprova");
                    break;
            }
        }
    }

    /// <summary>
    /// Displays a menu for changing game configuration settings related to numbers of different types of items
    /// </summary>
    public void ShowItemMenu()
    {
        // Get the assembly containing the types
        Assembly assembly = Assembly.GetExecutingAssembly();

        // Find all types that extend the base class "Item" but do not implement "IKeyItem"
        List<Type> nonKeyItems = [.. assembly.GetTypes().Where(type => type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(typeof(Item)) && !typeof(IKeyItem).IsAssignableFrom(type))];

        Configs defaultConfigs = new Configs();

        while (true)
        {
            GameConsole.WriteLine("Di che cosa vuoi cambiare il numero?");

            for (int i = 0; i < nonKeyItems.Count; i++)
            {
                GameConsole.WriteLine($"{i + 1}: {nonKeyItems[i].Name}");
            }
            GameConsole.WriteLine($"{nonKeyItems.Count + 1}: Torna indietro");

            if (!TryParse(GameConsole.ReadLine(), out int choice))
            {
                GameConsole.WriteLine("Scelta non riconosciuta, riprova");
                continue;
            }

            if (choice >= 1 && choice <= nonKeyItems.Count)
            {
                var selectedItemType = nonKeyItems[choice - 1];
                var property = typeof(Configs).GetProperty($"Num{selectedItemType.Name}");

                if (property != null)
                {
                    ChangeConfigValue(
                        $"Vuoi cambiare il numero di {selectedItemType.Name}? Default = {(int)(property.GetValue(defaultConfigs) ?? 0)},",
                        (int)(property.GetValue(configs) ?? 0),
                        value => property.SetValue(configs, value)
                    );
                }
            }
            else if (choice == nonKeyItems.Count + 1)
            {
                return;
            }
            else
            {
                GameConsole.WriteLine("Scelta non riconosciuta, riprova");
            }
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

                loaded = JsonSerializer.Deserialize<Configs>(json, jsonSerializerOptions) ?? new Configs();
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

    #region Private Methods

    /// <summary>
    /// Generic method to change a configuration value
    /// </summary>
    /// <typeparam name="T">The type of the configuration value</typeparam>
    /// <param name="prompt">The prompt message to display</param>
    /// <param name="currentValue">The current value of the configuration</param>
    /// <param name="setter">The action to set the new value</param>
    private void ChangeConfigValue<T>(string prompt, T currentValue, Action<T> setter) where T : struct
    {
        GameConsole.WriteLine($"{prompt} Attuale = {currentValue}");

        string input = GameConsole.ReadLine();

        if (typeof(T) == typeof(int) && TryParse(input, out int intValue))
        {
            setter((T)(object)intValue);
        }
        else if (typeof(T) == typeof(float) && float.TryParse(input, out float floatValue))
        {
            setter((T)(object)floatValue);
        }
        else
        {
            GameConsole.WriteLine("Valore non valido, riprova.");
        }
    }

    #endregion
}
