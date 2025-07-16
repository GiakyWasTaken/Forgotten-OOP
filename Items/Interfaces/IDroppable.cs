namespace Forgotten_OOP.Items.Interfaces;

using Forgotten_OOP.Mapping.Interfaces;

/// <summary>
/// Represents an item that can be dropped or placed in a specific location
/// </summary>
public interface IDroppable : IItem
{
    /// <summary>
    /// Drops the item in the specified room
    /// </summary>
    /// <param name="room"></param>
    public void Drop(IRoom room)
    {
        CurrentRoom = room;
    }
}
