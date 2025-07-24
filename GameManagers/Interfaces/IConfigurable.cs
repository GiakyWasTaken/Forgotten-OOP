namespace Forgotten_OOP.GameManagers.Interfaces;

/// <summary>
/// Interface for classes that can read and write configuration settings
/// </summary>
public interface IConfigurable
{
    /// <summary>
    /// Represents the configuration settings for the game
    /// </summary>
    public Configs Configs { get; set; }

    /// <summary>
    /// Reads the configuration settings from a file or other source
    /// </summary>
    /// <returns>A <see cref="Configs"/> struct containing the configuration settings</returns>
    public Configs ReadConfigs();

    /// <summary>
    /// Writes the specified configuration settings to the appropriate storage
    /// </summary>
    /// <param name="configs">The configuration settings to be written</param>
    public void WriteConfigs(Configs configs);
}