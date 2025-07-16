namespace Forgotten_OOP.Entities;

#region Using Directives

using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Enums;
using Forgotten_OOP.Mapping;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents an entity in the Forgotten OOP game
/// </summary>
public class Entity(string name, Room currentRoom) : IEntity
{
    #region Properties

    /// <inheritdoc />
    public string Name { get; } = name;

    /// <inheritdoc />
    public Room CurrentRoom { get; } = currentRoom;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void Move(Direction dir)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Teleport(IRoom room)
    {
        throw new NotImplementedException();
    }

    #endregion
}
