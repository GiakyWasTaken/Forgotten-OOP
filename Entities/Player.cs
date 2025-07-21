namespace Forgotten_OOP.Entities;

using Forgotten_OOP.Consoles;

#region Using Directives

using Forgotten_OOP.Entities.Interfaces;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Logging;
using Forgotten_OOP.Mapping;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;
#endregion

/// <summary>
/// Represents a player in the Forgotten OOP game
/// </summary>
public class Player(string name, Room startingRoom, Map<Room> gameMap, int lives) : Entity(name, startingRoom, gameMap), IPlayer<Room>
{
    #region Private Fields
    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();
    #endregion

    #region Attributes

    /// <inheritdoc />
    public int Lives { get; set; } = lives;

    /// <inheritdoc />
    public List<IGrabbable> KeyItems { get; } = [];

    /// <inheritdoc />
    public Stack<IStorable<Room>> Backpack { get; } = new();

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public float GetCurrentWeight()
    {
        return Backpack.Sum(item => item.Weight);
    }

    public void LifeChange(int change, GameManager game)
    {
        this.Lives += change;
        GameLogger.Log("player lives changed by " + change);
        if (this.Lives == 0) { 
        //TODO: Gameover message
        }

        if (change < 0)
        {
            game.Entities.ForEach(entity =>
            {
                if (entity is Enemy enemy)
                {
                    Room teleportTo;
                    bool check = true;
                    while (check)
                    {
                        teleportTo = game.GameMap.GetRandomRoom();
                        if (!teleportTo.IsPinkRoom && !teleportTo.Equals(this.CurrentRoom) && !teleportTo.Equals(enemy.CurrentRoom))
                        {
                            this.Teleport(teleportTo);
                            check = false;
                            GameLogger.Log("Teleported player to " + this.CurrentRoom + " after taking damage");
                        }
                    }
                }
            });
            
        }
    }

    #endregion
}