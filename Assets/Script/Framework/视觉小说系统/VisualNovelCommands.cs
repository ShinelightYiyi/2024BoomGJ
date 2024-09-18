using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands.VisualNovel
{
    public struct SaveCommand : ICommand
    {
        public void Execute()
        {
            Debug.Log("GameSaved");
            SaveSystem.SaveByJson("GameSave.sav", new GameSaveData().SaveData());

        }
    }
    /*public struct LoadCommand : ICommand
    {
        public void Execute()
        {
            Debug.Log("GameLoaded");
            GameSaveData = SaveSystem.LoadFromJson<GameSaveData>("GameSave.sav");
        }
    }
    */
}