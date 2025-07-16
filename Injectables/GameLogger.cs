namespace Forgotten_OOP.Injectables;

#region Using Directives

using Forgotten_OOP.Injectables.Interfaces;

#endregion

/// <summary>
/// A logger for the Forgotten OOP game
/// </summary>
public class GameLogger : ILogger
{
    #region Properties
    string FileName = "";
    #endregion

    #region Public Methods

    /// <inheritdoc />
    /// <summary>
    /// Creates the logs file
    /// </summary>
    public void InitializeLogger()
    {
        DateTime now = DateTime.Now;
        FileName = "Log - " + now;
        if(!File.Exists(FileName))
        {
            File.Create(name);
            Log("Game Launched");
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Logs a message in the logs file
    /// </summary>
    public void Log(string message)
    {
        DateTime now = DateTime.Now;
        File.AppendAllText(FileName, "["+now"]: "message);
        throw new NotImplementedException();
    }

    #endregion
}
