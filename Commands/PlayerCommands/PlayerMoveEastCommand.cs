namespace Forgotten_OOP.Commands.PlayerCommands;

#region Using Directives

using Forgotten_OOP.Enums;
using Forgotten_OOP.GameManagers;

#endregion

/// <summary>
/// Represents a command to move the player east within the game map
/// </summary>
public class PlayerMoveEastCommand(GameManager game) : PlayerMoveCommand(game, Direction.East);