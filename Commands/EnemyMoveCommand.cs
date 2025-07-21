namespace Forgotten_OOP.Commands;


#region Using Directives

using Forgotten_OOP.Consoles;
using Forgotten_OOP.Entities;
using Forgotten_OOP.Enums;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Logging.Interfaces;
using Forgotten_OOP.Mapping;
using Forgotten_OOP.Consoles.Interfaces;
using System.Runtime.CompilerServices;

#endregion

/// <summary>
/// Represents a command to use an item from the player's inventory within the game
/// </summary>
public class EnemyMoveCommand(GameManager game, Direction dir) : BaseCommand
{
    #region Private Fields
    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();
    #endregion


    #region Properties

    /// <inheritdoc />
    public override string Name => "Move Enemy";

    /// <inheritdoc />
    public override string Description => "Moves the enemy";

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        if (GetAvailability())
        {
            game.Entities.ForEach(entity =>
            {
                if (entity is Enemy enemy)
                {
                    enemy.Move(dir);
                    GameLogger.Log("Enemy moved in direction " +dir);
                }
            });
        }
    }

    #endregion

    #region Protected Methods

    /// <inheritdoc />
    protected override bool GetAvailability()
    {
        bool check = false;
        game.Entities.ForEach(entity =>
        {
            if (entity is Enemy enemy)
            {
                game.GameMap.TryGetRoomInDirection(enemy.CurrentRoom, dir, out Room? room);
               check = true;
            }
        });
        
        return check;
    }
    #endregion
}