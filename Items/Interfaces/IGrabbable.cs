namespace Forgotten_OOP.Items.Interfaces;

/// <summary>
/// Represents an item that can be grabbed or interacted with
/// </summary>
public interface IGrabbable : IItem
{
    /// <summary>
    /// Grabs the item, typically adding it to a player's inventory or backpack
    /// </summary>
    public void Grab();
}