namespace Forgotten_OOP.Injectables;

#region Using Directives

using Forgotten_OOP.Injectables.Interfaces;

#endregion

/// <summary>
/// A logger for the Forgotten OOP game
/// </summary>
public class GameLogger : ILogger
{
    #region Attributes

    /// <summary>
    /// Represents the name of the file associated with this instance
    /// </summary>
    private string fileName = "";

    /// <summary>
    /// A boolean indicating whether the logger has been initialized
    /// </summary>
    private bool isInitialized;

    #endregion

    #region Public Methods

    /// <inheritdoc />
    public void InitializeLogger()
    {
        if(!Directory.Exists("Logs"))
        {
            Directory.CreateDirectory("Logs");
        }

        if (!Directory.Exists("Logs/Archive"))
        {
            Directory.CreateDirectory("Logs/Archive");
        }

        foreach (string file in Directory.GetFiles("Logs"))
        {
            string fileToMove = Path.GetFileName(file);
            string destination = Path.Combine("Logs/Archive", fileToMove);

            if (!File.Exists(destination))
            {
                File.Move(file, destination);
            }
        }

        DateTime now = DateTime.Now;

        fileName = "Logs/Log - " + now.ToString("dd-MM-yyyy H-m-s") + ".log";

        if (!File.Exists(fileName))
        {
            FileStream stream = File.Create(fileName);

            stream.Close();

            isInitialized = true;

            Log("Game Launched");
        }
    }

    /// <inheritdoc />
    public void Log(string message)
    {
        if (!isInitialized)
        {
            throw new InvalidOperationException("Logger is not initialized. Call InitializeLogger() first.");
        }

        DateTime now = DateTime.Now;
        File.AppendAllText(fileName, "[" + now + "]: " + message + "\n");
    }

    #endregion
}
