namespace Forgotten_OOP.Commands.PlayerCommands;

#region Using Directives

using Forgotten_OOP.Enums;
using Forgotten_OOP.GameManagers;

#endregion

/// <summary>
/// Represents a command to move the player north within the game map
/// </summary>
public class PlayerMoveNorthCommand(GameManager game) : PlayerMoveCommand(game, Direction.North);