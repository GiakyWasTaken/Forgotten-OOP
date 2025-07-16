namespace Forgotten_OOP.Items.Interfaces;

/// <summary>
/// Represents an item that can be grabbed or interacted with
/// </summary>
public interface IGrabbable : IItem
{
    /// <summary>
    /// Grabs the item, removing it from its current room
    /// </summary>
    public void Grab()
    {
        CurrentRoom = null;
    }
}