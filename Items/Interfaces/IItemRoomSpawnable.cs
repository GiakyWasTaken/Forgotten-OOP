namespace Forgotten_OOP.Items.Interfaces;

#region Using Directives

using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// An interface for items that can spawn in specific rooms
/// </summary>
public interface IItemRoomSpawnable<in TRoom> : IItem where TRoom : IRoom
{
    /// <summary>
    /// Determines whether an entity can spawn in the specified room
    /// </summary>
    /// <param name="room">The <see cref="TRoom"/> to check for spawn eligibility</param>
    /// <returns><see langword="true"/> if the entity can spawn in the specified room; otherwise, <see langword="false"/></returns>
    public bool CanSpawnInRoom(TRoom room);
}