namespace Forgotten_OOP.Entities.Interfaces;

#region Using Directives

using System.Collections.Generic;

#endregion

/// <summary>
/// Interface representing an entity that can be followed by other entities
/// </summary>
public interface IFollowable
{
    /// <summary>
    /// Gets or sets the list of entities following the player
    /// </summary>
    public List<Entity> FollowingEntities { get; set; }
}

