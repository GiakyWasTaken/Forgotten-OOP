namespace Forgotten_OOP.GameManagers;

#region Using Directives

using Forgotten_OOP.GameManagers.Interfaces;

#endregion

/// <summary>
/// The main menu of the Forgotten OOP game
/// </summary>
public class MainMenu : IMainMenu
{
    #region Public Methods

    /// <inheritdoc />
    public void Show()
    {
        Console.WriteLine("Welcome to Forgotten OOP!");
        Console.WriteLine("1. Play");
        Console.WriteLine("2. Settings");
        Console.WriteLine("3. Exit");
        Console.Write("Please select an option: ");

        string? choice = Console.ReadLine();

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
        GameManager game = new(new Configs());
        game.StartGameLoop();
    }

    /// <inheritdoc />
    public void Settings()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Exit()
    {
        Console.WriteLine("Thank you for playing Forgotten OOP!");
        Environment.Exit(0);
    }

    #endregion
}