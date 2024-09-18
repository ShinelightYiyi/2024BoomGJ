using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Framework;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    private LoadSceneManager() { }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void LoadSceneAsyn(string sceneName)
    {
        MonoManager.Instance.ExecuteCoroutine(RealLoadSceneAsyn(sceneName));
    }

    IEnumerator RealLoadSceneAsyn(string sceneName)
    {
        float disProgress = 0f;
        float currentProgress = 0f;

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while(currentProgress <0.9f)
        {
            currentProgress = async.progress;
            while(disProgress < currentProgress)
            {
                disProgress += 0.01f;
                EventCenter.Instance.Publish<float>("进度加载", disProgress);
            }
            yield return currentProgress;
        }

        while(disProgress <= 1f)
        {
            disProgress += 0.01f;
            EventCenter.Instance.Publish<float>("进度加载", disProgress);
            yield return disProgress;
        }

        while(!async.isDone)
        {
            EventCenter.Instance.Publish<float>("进度加载", 1f);
            Debug.Log("转场完成");
            if(disProgress >= 0.9f)
            {
                async.allowSceneActivation = true;
            }

            yield return async.progress;
        }



    }




}
