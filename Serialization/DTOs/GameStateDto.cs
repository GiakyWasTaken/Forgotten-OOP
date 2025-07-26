namespace Forgotten_OOP.Serialization.DTOs;

#region Using Directives

using System.Collections.Generic;

using Forgotten_OOP.GameManagers;

#endregion

/// <summary>
/// Data transfer object representing the complete state of a game session
/// </summary>
public class GameStateDto
{
    #region Properties

    /// <summary>
    /// Gets or sets the game configuration data
    /// </summary>
    public Configs GameConfigs { get; set; } = new();

    /// <summary>
    /// Gets or sets the player data
    /// </summary>
    public string PlayerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of entities in the game
    /// </summary>
    public List<EntityDto> Entities { get; set; } = [];

    /// <summary>
    /// Gets or sets the game map data
    /// </summary>>
    public MapDto<RoomDto> GameMap { get; set; } = new();

    /// <summary>
    /// Gets or sets the total number of actions performed in the game
    /// </summary>
    public long ActionsCount { get; set; } = 0;

    /// <summary>
    /// Gets or sets a value indicating whether the player and Marlo
    /// are encountering each other for the first time
    /// </summary>
    public bool IsFirstPlayerMarloEncounter { get; set; } = true;

    #endregion
}
