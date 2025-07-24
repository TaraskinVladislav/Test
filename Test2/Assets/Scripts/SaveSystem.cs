using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string RootDir = Application.persistentDataPath;
    private const string ProgressFileName = "progress.json";
    private const string SettingsFileName = "settings.json";
    public static void Save<T>(T data, string fileName)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true); 
            string fullPath = Path.Combine(RootDir, fileName);

            File.WriteAllText(fullPath, json);
#if UNITY_EDITOR
            Debug.Log($"[SaveSystem] Saved {fileName} to {fullPath}");
#endif
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveSystem] Save error ({fileName}): {e.Message}");
        }
    }
    public static T Load<T>(string fileName) where T : new()
    {
        try
        {
            string fullPath = Path.Combine(RootDir, fileName);

            if (!File.Exists(fullPath))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"[SaveSystem] File {fileName} not found. Using defaults.");
#endif
                return new T();
            }

            string json = File.ReadAllText(fullPath);
            return JsonUtility.FromJson<T>(json);
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveSystem] Load error ({fileName}): {e.Message}");
            return new T();
        }
    }
    public static void Delete(string fileName)
    {
        string fullPath = Path.Combine(RootDir, fileName);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    public static void SaveProgress(ProgressWrapper data) =>
        Save(data, ProgressFileName);

    public static ProgressWrapper LoadProgress() =>
        Load<ProgressWrapper>(ProgressFileName);

    public static void SaveSettings(UserSettings data) =>
        Save(data, SettingsFileName);

    public static UserSettings LoadSettings() =>
        Load<UserSettings>(SettingsFileName);
}

[Serializable]
public class UserSettings
{
    public float musicVolume = 0.7f;
    public float sfxVolume = 0.8f;
    public string languageIso = "ru";
}

