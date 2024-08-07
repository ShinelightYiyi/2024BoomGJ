using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ƶ������
/// </summary>
public class AudioManager : SingletonMono<AudioManager>
{
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    /// <summary>
    /// ������Ƶ
    /// </summary>
    /// <param name="path">�ļ�·��</param>
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
                Debug.LogWarning($" {path} ·���ļ�δ�ҵ�");
            }
        }
    }

    /// <summary>
    /// ��ȡ��Ƶ
    /// </summary>
    /// <param name="path">�ļ�·��</param>
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