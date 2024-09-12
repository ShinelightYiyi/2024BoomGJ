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
    /// <param name="input">���루��Ϊ·���ַ�����audioclip��</param>
    /// <param name="loop">�Ƿ�ѭ������</param>
    public void PlayBGM(object input, bool loop = true)
    {
        AudioClip clip = null;

        // �ж������������ַ�����·�������� AudioClip
        if (input is string path)
        {
            // ��·����ȡ��Ƶ����
            clip = AudioManager.Instance.GetAudioClip(path);
            if (clip == null)
            {
                Debug.LogWarning($"·�� '{path}' ��Ӧ�� BGM δ�ҵ�");
                return;
            }
        }
        else
        {
            // ���Խ�����ת��Ϊ AudioClip
            clip = input as AudioClip;

            // ���ת�����
            if (clip == null)
            {
                Debug.LogWarning($"�������ʹ��󣬱�����·���ַ����� AudioClip����ǰ�������ͣ�{input?.GetType().FullName}");
                return;
            }
        }

        // �����ƵԴ�Ƿ��ѳ�ʼ��
        if (bgmSource == null)
        {
            Debug.LogError("bgmSource δ��ʼ�����޷�������Ƶ��������ƵԴ�����á�");
            return;
        }

        // ���ű�������
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
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
    /// <param name="input">���루��Ϊ·���ַ�����audioclip��</param>
    public void PlaySFX(object input)
    {
        AudioClip clip = null;

        // �ж������������ַ�����·�������� AudioClip
        if (input is string path)
        {
            // ��·����ȡ��Ƶ����
            clip = AudioManager.Instance.GetAudioClip(path);
            if (clip == null)
            {
                Debug.LogWarning($"{path} ��Чδ�ҵ�");
                return;
            }
        }
        else
        {
            // ���Խ�����ת��Ϊ AudioClip
            clip = input as AudioClip;

            // ���ת�����
            if (clip == null)
            {
                Debug.LogWarning($"�������ʹ��󣬱�����·���ַ����� AudioClip����ǰ�������ͣ�{input?.GetType().FullName}");
                return;
            }
        }

        // �����ƵԴ�Ƿ��ѳ�ʼ��
        if (sfxSource == null)
        {
            Debug.LogError("sfxSource δ��ʼ�����޷�������Ч��������ƵԴ�����á�");
            return;
        }

        // ������Ч
        sfxSource.PlayOneShot(clip);
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
