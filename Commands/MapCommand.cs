﻿namespace Forgotten_OOP.Commands;

#region Using Directives

using Forgotten_OOP.Consoles.Interfaces;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Helpers;
using Forgotten_OOP.Items;
using Forgotten_OOP.Logging.Interfaces;

#endregion

/// <summary>
/// Represents a command to show help information in the Forgotten OOP game
/// </summary>
public class MapCommand(GameManager game) : BaseCommand, IConsolable, ILoggable
{
    #region Properties

    /// <inheritdoc />
    public ILogger GameLogger => ServiceHelper.GetService<ILogger>();

    /// <inheritdoc />
    public IConsole GameConsole => ServiceHelper.GetService<IConsole>();

    /// <inheritdoc />
    public override string Name => "Map";

    /// <inheritdoc />
    public override string Description => "Mostra la mappa. Tu sei [P]";

    /// <inheritdoc />
    public override bool IsAvailable => true;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public override void Execute()
    {
        bool showKeyAndMarlo = game.Player.KeyItems.OfType<Torch>().Any();

        game.GameMap.PrintMap([.. game.Entities], showPlayer: true, showKey: showKeyAndMarlo, showMarlo: showKeyAndMarlo);
    }

    #endregion
}