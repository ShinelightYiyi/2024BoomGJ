using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands.Menu
{
    public struct LoadSceneCommand : ICommand
    {
        string sceneName;
        public LoadSceneCommand(string name) 
        {
            sceneName = name;
        }

        public void Execute()
        {
            LoadSceneManager.Instance.LoadScene(sceneName);
        }
    }
    public struct BackCommand : ICommand
    {
        public void Execute()
        {
            MenuModel.Instance.PopUI();
            Debug.Log("·µ»Ø");
        }
    }
    public struct OpenMenu : ICommand
    {
        string menuPath;
        public OpenMenu(string path)
        {
            menuPath = path;
        }

        public void Execute()
        {
            MenuModel.Instance.PushUI(menuPath);
        }
    }


}
