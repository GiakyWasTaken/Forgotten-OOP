namespace Forgotten_OOP.Items.Interfaces;

#region Using Directives

using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Mapping.Interfaces;

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
    /// Gets the weight of the item
    /// </summary>
    public float Weight { get; }

    /// <summary>
    /// Gets or sets the current room in which the user is located
    /// </summary>
    public IRoom? CurrentRoom { get; set; }

    /// <summary>
    /// Uses the item in the context of the game
    /// </summary>
    /// <param name="game">The game manager instance to interact with</param>
    public void Use(GameManager game);
}