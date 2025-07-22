namespace Forgotten_OOP.Items.Interfaces;

#region Using Directives

using Forgotten_OOP.GameManagers;

#endregion

/// <summary>
/// Represents an item that can be grabbed or interacted with
/// </summary>
public interface IGrabbable : IItem
{
    /// <summary>
    /// Grabs the item, typically adding it to a player's inventory or backpack
    /// </summary>
    public void Grab(GameManager game);
}