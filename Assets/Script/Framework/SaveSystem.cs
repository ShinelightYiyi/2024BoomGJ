using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Framework
{
    public static class SaveSystem
    {
        #region PlayerPrefs
        public static void SaveByPlayerPrefs(string key, object data)
        {
            var json = JsonUtility.ToJson(data);

            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();

            Debug.Log("Successfully saved data to Playerprefs.");
        }
        public static string LoadFromPlayerPrefs(string key)
        {
            return PlayerPrefs.GetString(key, null);
        }
        #endregion

        #region Json
        public static void SaveByJson(string saveFileName, object data)
        {
            var json = JsonUtility.ToJson(data);
            var path = Path.Combine(Application.persistentDataPath, saveFileName);

            
            try
            {
                File.WriteAllText(path, json);

                #if UNITY_EDITOR
                Debug.Log($"Successfully Save Data To {path}");
                #endif

            }
            catch (System.Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"Failed To Save Data To {path}.\n{exception}");
                #endif
            }
        }

        public static T LoadFromJson<T>(string saveFileName)
        {
            var path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);
                return data;
            }
            catch (System.Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"Failed To Load Data From {path}.\n{exception}");
                #endif
                return default;
            }
        }

        #endregion

        #region Deleting
        public static void DeleteSaveFile(string saveFileName)
        {
            var path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                File.Delete(path);
            }
            catch (System.Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"Failed To Delete {path}.\n{exception}");
                #endif
            }
        }
        #endregion
    }
}