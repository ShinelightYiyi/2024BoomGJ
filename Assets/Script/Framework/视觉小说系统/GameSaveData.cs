using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameSaveData
{
    //public string currentNode;
    public string sceneName;
    public GameSaveData SaveData()
    {
        sceneName=SceneManager.GetActiveScene().name;
        return this;
    }
}
