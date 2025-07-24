namespace Forgotten_OOP.Menu.Interfaces;

/// <summary>
/// The interface for the main menu of the game
/// </summary>
public interface ISettingsMenu
{
    /// <summary>
    /// Shows the settings menu of the game
    /// </summary>
    public void Show();

    /// <summary>
    /// Exits the settings menu
    /// </summary>
    public void Exit();

    /// <summary>
    /// Changes the MapDimension config value
    /// </summary>
    public void ChangeMapDimension();

    /// <summary>
    /// Changes the MaxWeight config value
    /// </summary>
    public void ChangeMaxWeight();

    /// <summary>
    /// Changes the EnemyDelay config value
    /// </summary>
    public void ChangeEnemyDelay();

    /// <summary>
    /// Changes the NumKeys config value
    /// </summary>
    public void ChangeNumKeys();

    /// <summary>
    /// Changes the NumItems config value
    /// </summary>
    public void ChangeNumItems();
}