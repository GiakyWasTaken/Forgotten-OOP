namespace Forgotten_OOP.Items.Interfaces;

#region Using Directives

using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents an item with properties for name, description, weight, and current room location
/// </summary>
/// <remarks>This interface defines the basic properties that an item should have, including its name,
/// description, weight,  and the room it is currently located in. Implementations of this interface can represent
/// various types of items  within a system, such as inventory items in a game or objects in a simulation</remarks>
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