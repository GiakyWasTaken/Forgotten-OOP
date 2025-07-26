namespace Forgotten_OOP.Helpers;

#region Using Directives

using Forgotten_OOP.Enums;

#endregion

/// <summary>
/// Provides utility methods for <see cref="Direction"/> enumeration
/// </summary>
public static class DirectionsHelper
{
    /// <summary>
    /// Converts a <see cref="Direction"/> enumeration value to its corresponding abbreviation
    /// </summary>
    /// <param name="direction">The direction to convert. Must be one of the defined <see cref="Direction"/> values</param>
    /// <returns>A string representing the abbreviation of the specified direction</returns>
    public static string DirectionToAbbreviation(Direction direction)
    {
        return direction switch
        {
            Direction.North => "N",
            Direction.South => "S",
            Direction.East => "E",
            Direction.West => "W",
            _ => throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction value")
        };
    }

    /// <summary>
    /// Converts a <see cref="Direction"/> enumeration value to its corresponding string representation
    /// </summary>
    /// <param name="direction">The <see cref="Direction"/> value to convert</param>
    /// <returns>A string representing the specified direction. Returns "North", "South", "East", or "West"  based on the input
    /// value</returns>
    public static string DirectionToString(Direction direction)
    {
        return direction switch
        {
            Direction.North => "North",
            Direction.South => "South",
            Direction.East => "East",
            Direction.West => "West",
            _ => throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction value")
        };
    }
}