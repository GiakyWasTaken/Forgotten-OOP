namespace Forgotten_OOP.Items.Interfaces;

#region Using Directives

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
    /// Uses the item, performing an action associated with it
    /// </summary>
    public void Use();
}