namespace Forgotten_OOP.Items.Interfaces;

#region Using Directives

using Forgotten_OOP.GameManagers;

#endregion

/// <summary>
/// Represents an item with properties for name, description, weight, and current room location
/// </summary>
public interface IItem
{
    /// <summary>
    /// Gets the name associated with the current instance
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the description of the item
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Uses the item in the context of the game
    /// </summary>
    /// <param name="game">The game manager instance to interact with</param>
    public void Use(GameManager game);
}