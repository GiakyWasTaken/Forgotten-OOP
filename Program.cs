﻿namespace Forgotten_OOP;

#region Using Directives

using Forgotten_OOP.Consoles;
using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Menu;

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
