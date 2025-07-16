namespace Forgotten_OOP.Items.Interfaces;

/// <summary>
/// Represents an item that can be stored, which includes the ability to grab and drop it
/// </summary>
public interface IStorable : IGrabbable, IDroppable
{
}