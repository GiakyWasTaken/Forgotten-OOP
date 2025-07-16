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
        FileName = "Logs/Log - " + now.ToString("dd-MM-yyyy H-m-s") + ".txt";
        if(!File.Exists(FileName))
        {
            FileStream stream = File.Create(FileName);
            stream.Close();
            Log("Game Launched");
        }
    }

    /// <inheritdoc />
    public void Log(string message)
    {
        DateTime now = DateTime.Now;
        File.AppendAllText(FileName, "[" + now + "]: " + message + "\n");
    }

    #endregion
}
