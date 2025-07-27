namespace Forgotten_OOP.Items.Interfaces;

using Forgotten_OOP.Consoles;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Helpers;

#region Using Directives

using Forgotten_OOP.Mapping.Interfaces;

#endregion

/// <summary>
/// Represents an item that can be dropped or placed in a specific location
/// </summary>
public interface IDroppable<in TRoom> : IItem where TRoom : IRoom<TRoom>
{
    #region Private Fields

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    #endregion

    /// <summary>
    /// Drops the item in the specified room, making it available for other entities to pick up
    /// </summary>
    /// <param name="room">The room where the item will be dropped</param>
    public void Drop(TRoom room)
    {
        room.ItemsOnGround.Add(this);
        GameConsole.WriteLine("Lasci cadere "+this.Name);
    }
}
