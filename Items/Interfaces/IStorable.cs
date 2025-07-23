namespace Forgotten_OOP.Items.Interfaces;

#region Using Directives

using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents an item that can be stored, which includes the ability to grab and drop it
/// </summary>
public interface IStorable<in TRoom> : IGrabbable, IDroppable<TRoom> where TRoom : IRoom<TRoom>
{
    /// <summary>
    /// Gets the weight of the item
    /// </summary>
    public float Weight { get; }
}