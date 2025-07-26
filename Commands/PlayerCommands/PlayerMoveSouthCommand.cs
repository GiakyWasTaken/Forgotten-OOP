namespace Forgotten_OOP.Commands.PlayerCommands;

#region Using Directives

using Forgotten_OOP.Enums;
using Forgotten_OOP.GameManagers;

#endregion

/// <summary>
/// Represents a command to move the player south within the game map
/// </summary>
public class PlayerMoveSouthCommand(GameManager game) : PlayerMoveCommand(game, Direction.South);
