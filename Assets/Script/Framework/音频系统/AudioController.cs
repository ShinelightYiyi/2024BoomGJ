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
    /// <param name="input">输入（可为路径字符串或audioclip）</param>
    /// <param name="loop">是否循环播放</param>
    public void PlayBGM(object input, bool loop = true)
    {
        AudioClip clip = null;

        // 判断输入类型是字符串（路径）还是 AudioClip
        if (input is string path)
        {
            // 从路径获取音频剪辑
            clip = AudioManager.Instance.GetAudioClip(path);
            if (clip == null)
            {
                Debug.LogWarning($"路径 '{path}' 对应的 BGM 未找到");
                return;
            }
        }
        else
        {
            // 尝试将输入转换为 AudioClip
            clip = input as AudioClip;

            // 检查转换结果
            if (clip == null)
            {
                Debug.LogWarning($"输入类型错误，必须是路径字符串或 AudioClip。当前输入类型：{input?.GetType().FullName}");
                return;
            }
        }

        // 检查音频源是否已初始化
        if (bgmSource == null)
        {
            Debug.LogError("bgmSource 未初始化，无法播放音频。请检查音频源的设置。");
            return;
        }

        // 播放背景音乐
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
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
    /// <param name="input">输入（可为路径字符串或audioclip）</param>
    public void PlaySFX(object input)
    {
        AudioClip clip = null;

        // 判断输入类型是字符串（路径）还是 AudioClip
        if (input is string path)
        {
            // 从路径获取音频剪辑
            clip = AudioManager.Instance.GetAudioClip(path);
            if (clip == null)
            {
                Debug.LogWarning($"{path} 音效未找到");
                return;
            }
        }
        else
        {
            // 尝试将输入转换为 AudioClip
            clip = input as AudioClip;

            // 检查转换结果
            if (clip == null)
            {
                Debug.LogWarning($"输入类型错误，必须是路径字符串或 AudioClip。当前输入类型：{input?.GetType().FullName}");
                return;
            }
        }

        // 检查音频源是否已初始化
        if (sfxSource == null)
        {
            Debug.LogError("sfxSource 未初始化，无法播放音效。请检查音频源的设置。");
            return;
        }

        // 播放音效
        sfxSource.PlayOneShot(clip);
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
