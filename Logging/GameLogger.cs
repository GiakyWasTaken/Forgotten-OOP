﻿namespace Forgotten_OOP.Logging;

#region Using Directives

using System.Diagnostics;
using System.Reflection;

using Forgotten_OOP.Logging.Interfaces;

#endregion

/// <summary>
/// A logger for the Forgotten OOP game
/// </summary>
public class GameLogger : ILogger
{
    #region Private Fields

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
        if (isInitialized)
        {
            return;
        }

        Directory.CreateDirectory("Logs/Archive");

        foreach (string file in Directory.GetFiles("Logs"))
        {
            string fileToMove = Path.GetFileName(file);
            string destination = Path.Combine("Logs/Archive", fileToMove);

            if (!File.Exists(destination) && fileToMove.EndsWith(".log"))
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

        // Include the name of the calling function
        MethodBase? callingMethod = new StackTrace().GetFrame(1)?.GetMethod();

        string callingMethodName = callingMethod?.Name ?? "Unknown Method";
        string callingClassName = callingMethod?.DeclaringType?.Name ?? "Unknown Class";

        File.AppendAllText(fileName, $"[{now}] {callingMethodName} @ {callingClassName}: {message.Trim()}\n");
    }

    #endregion
}
