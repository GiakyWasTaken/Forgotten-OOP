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
    string FileName="";
    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void InitializeLogger()
    {
        DateTime now = DateTime.Now;
        FileName = "Log - " + now + ".txt";
        if(!File.Exists(FileName))
        {
            File.Create(FileName);
            Log("Game Launched");
        }
    }

    /// <inheritdoc />
    public void Log(string message)
    {
        DateTime now = DateTime.Now;
        File.AppendAllText(FileName, "[" + now + "]: " + message);
    }

    #endregion
}
