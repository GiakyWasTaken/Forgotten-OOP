namespace Forgotten_OOP.Entities.Interfaces;

#region Using Directives

using Forgotten_OOP.Enums;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Interface representing an entity in the game
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets the name of the entity
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the current room of the entity
    /// </summary>
    public IRoom CurrentRoom { get; }

    /// <summary>
    /// Moves the object in the specified direction
    /// </summary>
    /// <param name="dir">The <see cref="Direction"/> in which to move the object</param>
    public void Move(Direction dir);

    /// <summary>
    /// Moves the entity to the specified room
    /// </summary>
    /// <param name="room">The target room to which the entity will be teleported</param>
    public void Teleport(IRoom room);
}