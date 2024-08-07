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
    /// ����BGM
    /// </summary>
    /// <param name="path">�ļ�·��</param>
    /// <param name="loop">�Ƿ�ѭ������</param>
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
            Debug.LogWarning($" {path} BGMδ�ҵ�");
        }
    }

    /// <summary>
    /// ��ͣ/�ָ�����BGM
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
    /// ֹͣ����BGM
    /// </summary>
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="path">�ļ�·��</param>
    public void PlaySFX(string path)
    {
        AudioClip clip = AudioManager.Instance.GetAudioClip(path);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"{path} ��Чδ�ҵ�");
        }
    }

    /// <summary>
    /// ����BGM����(int v /100)
    /// </summary>
    /// <param name="volume">������С</param>
    public void SetBGMVolume(int volume)
    {
        bgmSource.volume = volume/100f;
    }

    /// <summary>
    /// ������Ч����(int v /100)
    /// </summary>
    /// <param name="volume">������С</param>
    public void SetSFXVolume(int volume)
    {
        sfxSource.volume = volume/100f;
    }
}
