namespace Forgotten_OOP.Helpers;

#region Using Directives

using System;

using Microsoft.Extensions.DependencyInjection;

#endregion

/// <summary>
/// A service locator for global access to services
/// </summary>
public static class ServiceHelper
{
    #region Properties

    /// <summary>
    /// The service provider instance used to resolve services
    /// </summary>
    public static IServiceProvider? Instance { get; private set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the global service provider instance
    /// </summary>
    /// <param name="provider">The service provider to be set as the global instance</param>
    public static void SetProvider(IServiceProvider provider)
    {
        Instance = provider;
    }

    /// <summary>
    /// Gets a service of type T from the global service provider instance
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve</typeparam>
    /// <returns>A service of type T</returns>
    /// <exception cref="InvalidOperationException">Thrown if the service locator is not initialized</exception>
    public static T GetService<T>() where T : notnull
    {
        if (Instance is null)
        {
            throw new InvalidOperationException("ServiceHelper is not initialized.");
        }

        return Instance.GetRequiredService<T>();
    }

    #endregion
}
