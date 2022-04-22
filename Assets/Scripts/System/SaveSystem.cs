using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    #region JSON
    public static void SaveByJson(string saveFileName,object data) {
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        
        try {
            File.WriteAllText(path, json);
#if UNITY_EDITOR
            Debug.Log($"Successfully save data to {path}.");
#endif
        }
        catch (System.Exception) {
#if UNITY_EDITOR
            Debug.Log($"Failed to save data to {path}.");
#endif
        }
    }

    public static T LoadFromJson<T>(string saveFileName) {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        try {
            var json= File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);
            return data;
        }
        catch(System.Exception) {
#if UNITY_EDITOR
            Debug.Log($"Failed to load data from {path}.");
#endif
            return default;
        }
    }

    public static void DeleteSaveFile(string saveFileName) {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        try {
            File.Delete(path);
        }
        catch (System.Exception) {
#if UNITY_EDITOR
            Debug.Log($"Failed to delete {path}.");
#endif
        }

    }
    #endregion

    public static bool SaveFileExists(string saveFileName) {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        return File.Exists(path);
    }
}
