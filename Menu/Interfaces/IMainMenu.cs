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
    /// Saves the game state to a file
    /// </summary>
    public void Save();

    /// <summary>
    /// Loads the game state from a file
    /// </summary>
    public void Load();

    /// <summary>
    /// Exits the game
    /// </summary>
    public void Exit();
}