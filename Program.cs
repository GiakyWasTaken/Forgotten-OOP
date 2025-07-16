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
    public static void Main()
    {
        ServiceCollection services = new(); // TODO Fix GameLogger

        services.AddScoped<ILogger, GameLogger>();
        services.AddScoped<IConsole, GameConsole>();
        services.AddTransient<GameManager>();

        ServiceProvider provider = services.BuildServiceProvider();

        ServiceHelper.SetProvider(provider);

        MainMenu mainMenu = new();
        mainMenu.Show();
    }

    #endregion
}
