namespace Forgotten_OOP.Entities.Interfaces;



#region Using Directives

using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Mapping.Interfaces;
using Forgotten_OOP.Enums;
using Forgotten_OOP.GameManagers;

#endregion

/// <summary>
/// Interface representing a player in the game
/// </summary>
public interface IPlayer<TRoom> : IEntity<TRoom> where TRoom : IRoom<TRoom>
{
    /// <summary>
    /// Gets or sets the player's lives
    /// </summary>
    public int Lives { get; set; }

    /// <summary>
    /// Gets or sets the list of items that the player has
    /// </summary>
    public List<IGrabbable> KeyItems { get; }

    /// <summary>
    /// Gets or sets the backpack of the player, which contains storable items
    /// </summary>
    public Stack<IStorable<TRoom>> Backpack { get; }

    /// <summary>
    /// Gets the current weight of the items in the backpack
    /// </summary>
    /// <returns>The sum of the weights of all items in the backpack</returns>
    public float GetCurrentWeight();

    /// <summary>
    /// Changes player's lives
    /// </summary>
    public void LifeChange(int change, GameManager game);

}