namespace Forgotten_OOP.Entities;

#region Using Directives

using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents a player in the Forgotten OOP game
/// </summary>
public class Player(string name, IRoom startingRoom, IMap<IRoom> gameMap, int lives) : Entity(name, startingRoom, gameMap), IPlayer
{
    #region Attributes

    /// <inheritdoc />
    public int Lives { get; set; } = lives;

    /// <inheritdoc />
    public List<IGrabbable> KeyItems { get; } = [];

    /// <inheritdoc />
    public Stack<IStorable> Backpack { get; } = new();

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public float GetCurrentWeight()
    {
        return Backpack.Sum(item => item.Weight);
    }

    #endregion
}