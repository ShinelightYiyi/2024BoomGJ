using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频管理器
/// </summary>
public class AudioManager : SingletonMono<AudioManager>
{
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    /// <summary>
    /// 加载音频
    /// </summary>
    /// <param name="path">文件路径</param>
    public void LoadAudioClip(string path)
    {
        if (!audioClips.ContainsKey(path))
        {
            AudioClip clip = Resources.Load<AudioClip>(path);
            if (clip != null)
            {
                audioClips[path] = clip;
            }
            else
            {
                Debug.LogWarning($" {path} 路径文件未找到");
            }
        }
    }

    /// <summary>
    /// 获取音频
    /// </summary>
    /// <param name="path">文件路径</param>
    /// 
    /// <returns></returns>
    public AudioClip GetAudioClip(string path)
    {
        if (audioClips.ContainsKey(path))
        {
            return audioClips[path];
        }
        return null;
    }
}