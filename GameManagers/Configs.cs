namespace Forgotten_OOP.GameManagers;

/// <summary>
/// The configuration settings for the Forgotten OOP game
/// </summary>
[Serializable]
public struct Configs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Configs"/> class
    /// </summary>
    public Configs() { }

    /// <summary>
    /// The dimension of the map, representing the max number of rooms in each direction
    /// </summary>
    public int MapDimension { get; set; } = 7;

    /// <summary>
    /// The maximum weight of items that can be carried by the player
    /// </summary>
    public float MaxWeight { get; set; } = 10f;

    /// <summary>
    /// The number of actions before the enemy can move
    /// </summary>
    public int EnemyDelay { get; set; } = 3;

    /// <summary>
    /// The number of spawnable keys in the game
    /// </summary>
    public int NumKeys { get; set; } = 1;

    /// <summary>
    /// The number of spawnable bandage items in the game
    /// </summary>
    public int NumBandage { get; set; } = 3;

    /// <summary>
    /// The number of spawnable guardian eye items in the game
    /// </summary>
    public int NumGuardianEye { get; set; } = 1;

    /// <summary>
    /// The number of spawnable repellent items in the game
    /// </summary>
    public int NumRepellent { get; set; } = 2;

    /// <summary>
    /// The number of spawnable teleport vials in the game
    /// </summary>
    public int NumTeleportVial { get; set; } = 2;
}