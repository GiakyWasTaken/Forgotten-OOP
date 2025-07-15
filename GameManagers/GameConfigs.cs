namespace Forgotten_OOP.GameManagers;

/// <summary>
/// The configuration settings for the Forgotten OOP game
/// </summary>
public struct GameConfigs()
{
    /// <summary>
    /// The maximum weight of items that can be carried by the player
    /// </summary>
    public float MaxWeight = 10f;

    /// <summary>
    /// The number of actions before the enemy can move
    /// </summary>
    public int EnemyDelay = 3;

    /// <summary>
    /// The number of spawnable keys in the game
    /// </summary>
    public int NumKeys = 1;

    /// <summary>
    /// The number of spawnable items in the game
    /// </summary>
    public int NumItems = 10;
}