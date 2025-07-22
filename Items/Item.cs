namespace Forgotten_OOP.Items;

#region Using Directives

using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Items.Interfaces;

#endregion

/// <summary>
/// Represents an item with properties for name, description, weight, and current room location
/// </summary>
public abstract class Item(string name, string description, float weight)
    : IItem
{
    #region Properties

    /// <inheritdoc />
    public string Name { get; } = name;

    /// <inheritdoc />
    public string Description { get; } = description;

    /// <inheritdoc />
    public float Weight { get; } = weight;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public abstract void Use(GameManager game);

    #endregion
}