namespace Forgotten_OOP.Mapping;

#region Using Directives

using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents a room in the Forgotten OOP game
/// </summary>
public class Room(long id, bool isStartingRoom = false, bool isPinkRoom = false) : IRoom
{
    #region Private Fields

    /// <summary>
    /// Represents the unique identifier for an entity
    /// </summary>
    private readonly long id = id;

    #endregion

    #region Properties

    /// <inheritdoc />
    public Stack<IItem> ItemsOnGround { get; } = new();

    /// <inheritdoc />
    public bool IsStartingRoom { get; } = isStartingRoom;

    /// <inheritdoc />
    public bool IsPinkRoom { get; set; } = isPinkRoom;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public bool Equals(IRoom? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return other.GetType() == GetType() && Equals((Room)other);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((Room)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(id, ItemsOnGround);
    }

    #endregion

    #region Protected Methods
    protected bool Equals(Room other)
    {
        return id == other.id && ItemsOnGround.Equals(other.ItemsOnGround);
    }

    #endregion
}
