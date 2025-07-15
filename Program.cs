namespace Forgotten_OOP;

#region Using Directives

using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Injectables;
using Forgotten_OOP.Injectables.Interfaces;
using Microsoft.Extensions.DependencyInjection;

#endregion

/// <summary>
/// The main class for the Forgotten OOP project
/// </summary>
public class Program
{
    #region Public Methods

    /// <summary>
    /// The entry point of the application
    /// </summary>
    /// <param name="args">An array of command-line arguments</param>
    public static void Main(string[] args)
    {
        ServiceCollection services = new();

        services.AddSingleton<ILogger, GameLogger>();
        services.AddSingleton<IConsole, GameConsole>();
        services.AddTransient<GameManager>();

        ServiceProvider provider = services.BuildServiceProvider();

        ServiceHelper.SetProvider(provider);

        MainMenu mainMenu = new();
        mainMenu.Show();
    }

    #endregion
}
