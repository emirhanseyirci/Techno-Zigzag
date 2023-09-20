using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogToFile : MonoBehaviour
{

    private string logPath;

    void Start()
    {
        logPath = Application.dataPath + "/debugLog.txt"; // Unity proje klasörünün içine kaydet
        Debug.Log("Debug logs are being saved to a file: " + logPath);
        File.WriteAllText(logPath, ""); // Dosyanın içeriğini sıfırla

        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        using (StreamWriter writer = File.AppendText(logPath))
        {
            writer.WriteLine("[" + type + "] " + logString);
        }
    }

}





