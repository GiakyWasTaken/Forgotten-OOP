namespace Forgotten_OOP.Serialization.DTOs;

/// <summary>
/// Data transfer object representing an entity in the game
/// </summary>
public class EntityDto
{
    #region Properties

    /// <summary>
    /// Gets or sets the name of the entity
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the room where the entity is currently located
    /// </summary>
    public long CurrentRoomId { get; set; } = -1;

    /// <summary>
    /// Gets or sets the type name of the entity
    /// </summary>
    public string EntityType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of lives remaining for the player (null for non-players)
    /// </summary>
    public int Lives { get; set; } = 3;

    /// <summary>
    /// Gets or sets the collection of item type names in the player's inventory (null for non-players)
    /// </summary>
    public List<string> Inventory { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of entities that are following the player (null for non-players)
    /// </summary>
    public List<string> FollowingEntities { get; set; } = [];

    /// <summary>
    /// Gets or sets the ID of the initial room where the entity started (null for non-marlo entities)
    /// </summary>
    public long? InitialRoomId { get; set; }

    /// <summary>
    /// Gets or sets the action delay for enemy entities (null for non-enemy entities)
    /// </summary>
    public int? ActionDelay { get; set; }

    /// <summary>
    /// Gets or sets the previous room where the entity was located (null for non-enemy entities)
    /// </summary>
    public long? PreviousRoomId { get; set; }

    #endregion
}
