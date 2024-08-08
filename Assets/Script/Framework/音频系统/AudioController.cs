using UnityEngine;

public class AudioController : SingletonMono<AudioController>
{
    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="loop">是否循环播放</param>
    public void PlayBGM(string path, bool loop = true)
    {
        AudioClip clip = AudioManager.Instance.GetAudioClip(path);
        if (clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($" {path} BGM未找到");
        }
    }

    /// <summary>
    /// 暂停/恢复播放BGM
    /// </summary>
    public void PauseBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Pause();
        }
        else
        {
            bgmSource.UnPause();
        }
    }

    /// <summary>
    /// 停止播放BGM
    /// </summary>
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="path">文件路径</param>
    public void PlaySFX(string path)
    {
        AudioClip clip = AudioManager.Instance.GetAudioClip(path);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"{path} 音效未找到");
        }
    }

    /// <summary>
    /// 设置BGM音量(int v /100)
    /// </summary>
    /// <param name="volume">音量大小</param>
    public void SetBGMVolume(int volume)
    {
        bgmSource.volume = volume/100f;
    }

    /// <summary>
    /// 设置音效音量(int v /100)
    /// </summary>
    /// <param name="volume">音量大小</param>
    public void SetSFXVolume(int volume)
    {
        sfxSource.volume = volume/100f;
    }
}
