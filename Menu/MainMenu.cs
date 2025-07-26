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
                    GameConsole.WriteLine("Ti trovi poco fuori il villaggio di Kuroka, hai trovato una grotta con un ingresso ad un dungeon di classe di classe S, uno tra i più pericolosi in assoluto. Per questo motivo, l'entrata principale è stata sbarrata da tante travi di legno che sembravano essere state fissate in fretta e furia. Nessuno di inesperto dovrebbe addentrarsi qui dentro, soprattutto tu, un cercatore di livello decisamente più basso rispetto a quello richiesto. Ma non puoi tirarti indietro, tuo fratello è intrappolato lì, è l'unica perona che ti rimane e non vuoi perderlo. Trovi un'entrata secondaria, per farti coraggio decidi di rileggere la lettera che di aiuto che Takumi ti ha mandato:\n" + "\"Fratello\r\n" +
                        "        Spero che questa lettera ti raggiunga in tempo.\r\n        " +
                        "Sono ferito. C'è qualcosa qui… qualcosa che non dovrebbe esistere.\r\n        " +
                        "Si aggira tra queste stanze come se fosse casa sua.\r\n        " +
                        "Non provare ad affrontarlo. Non puoi.\r\n        " +
                        "Porta con te la Panacea. È l’unica cosa che può salvarmi.\r\n        " +
                        "Trovami. salvami. Fai in fretta.\r\nTakumi.");
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