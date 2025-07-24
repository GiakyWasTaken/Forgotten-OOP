namespace Forgotten_OOP.Menu.Interfaces;

/// <summary>
/// The interface for the main menu of the game
/// </summary>
public interface IMainMenu
{
    /// <summary>
    /// Shows the main menu of the game
    /// </summary>
    public void Show();

    /// <summary>
    /// Starts the game
    /// </summary>
    public void Play();

    /// <summary>
    /// Opens the settings menu
    /// </summary>
    public void Settings();

    /// <summary>
    /// Exits the game
    /// </summary>
    public void Exit();
}