using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// “Ï≤Ωº”‘ÿ∆˜
/// </summary>
public class AsyncLoader
{
    public Slider progressBar;
    public Text progressText;

    public void LoadSceneAsync(string sceneName)
    {
        MonoManager.Instance.ExecuteCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressBar.value = progress;
            progressText.text = (progress * 100).ToString("F2") + "%";

            yield return null;
        }
    }

    public void LoadResourceAsync(string resourcePath)
    {
        MonoManager.Instance.ExecuteCoroutine(LoadResourceCoroutine(resourcePath));
    }

    private IEnumerator LoadResourceCoroutine(string resourcePath)
    {
        ResourceRequest resourceRequest = Resources.LoadAsync(resourcePath);

        while (!resourceRequest.isDone)
        {
            progressBar.value = resourceRequest.progress;
            progressText.text = (resourceRequest.progress * 100).ToString("F2") + "%";

            yield return null;
        }

        GameObject resource = resourceRequest.asset as GameObject;
        GameObject.Instantiate(resource);
    }
}
