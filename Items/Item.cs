namespace Forgotten_OOP.Items;

using Forgotten_OOP.GameManagers;

#region Using Directives

using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Mapping.Interfaces;

#endregion

public class Item(string name, string description, float weight, IRoom? currentRoom = null)
    : IItem
{
    #region Properties

    /// <inheritdoc />
    public string Name { get; } = name;

    /// <inheritdoc />
    public string Description { get; } = description;

    /// <inheritdoc />
    public float Weight { get; } = weight;

    /// <inheritdoc />
    public IRoom? CurrentRoom { get; set; } = currentRoom;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public virtual void Use(GameManager game)
    {
        throw new NotImplementedException();
    }

    #endregion
}