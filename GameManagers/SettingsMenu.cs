namespace Forgotten_OOP.GameManagers;

#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging;
using Forgotten_OOP.Logging.Interfaces;
#endregion

public class SettingsMenu : ISettingsMenu, IConfigurable
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
    public void ChangeEnemyDelay()
    {
        GameConsole.WriteLine("Vuoi cambiare l'Enemy Delay? Default = 3, Attuale = "+ReadConfigs().EnemyDelay);
        string input = GameConsole.ReadLine();
        if(int.TryParse(input, out _))
        {
            configs.EnemyDelay = int.Parse(input);
        }
    }

    public void ChangeMapDimension()
    {
        GameConsole.WriteLine("Vuoi cambiare la dimensione della mappa? Default = 7, Attuale = " + ReadConfigs().MapDimension);
        string input = GameConsole.ReadLine();
        if (int.TryParse(input, out _))
        {
            configs.MapDimension = int.Parse(input);
        }
    }

    public void ChangeMaxWeight()
    {
        GameConsole.WriteLine("Vuoi cambiare il peso massimo trasportabile? Default = 10.0, Attuale = " + ReadConfigs().MaxWeight);
        string input = GameConsole.ReadLine();
        if (float.TryParse(input, out _))
        {
            configs.MaxWeight = float.Parse(input);
        }
    }

    public void ChangeNumItems()
    {
        GameConsole.WriteLine("Vuoi cambiare il numero di Item generati nelle stanze? Default = 10, Attuale = " + ReadConfigs().NumItems);
        string input = GameConsole.ReadLine();
        if (int.TryParse(input, out _))
        {
            configs.NumItems = int.Parse(input);
        }
    }

    public void ChangeNumKeys()
    {
        GameConsole.WriteLine("Vuoi cambiare il numero di chiavi generate nelle stanze? Default = 10, Attuale = " + ReadConfigs().NumKeys);
        string input = GameConsole.ReadLine();
        if (int.TryParse(input, out _))
        {
            configs.NumKeys = int.Parse(input);
        }
    }

    public void Exit()
    {
        MainMenu mainMenu = new();
        WriteConfigs(configs);
        mainMenu.Show();
    }

    public void Show()
    {
        while(true)
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
            switch(choice)
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
                    break;
                default:
                    GameConsole.WriteLine("Scelta non riconosciuta, riprova");
                    break;
            }
            break;
        }
        Exit();
    }

    /// <inheritdoc />
    public Configs ReadConfigs()
    {
        // Obtains "configs.json" path
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs.json");

        if (File.Exists(configPath))
        {
            string json = File.ReadAllText(configPath);
            Configs loaded = JsonSerializer.Deserialize<Configs>(json);
            return loaded;
        }

        GameLogger.Log("Configuration file not found. Using default values");

        return new Configs();
    }

    /// <inheritdoc />
    public void WriteConfigs(Configs newConfigs)
    {
        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs.json");

        // Creation of .json file
        string json = JsonSerializer.Serialize(newConfigs, jsonSerializerOptions);

        File.WriteAllText(configPath, json);

        GameLogger.Log("Configuration file written successfully");
    }

    #endregion
    
}
