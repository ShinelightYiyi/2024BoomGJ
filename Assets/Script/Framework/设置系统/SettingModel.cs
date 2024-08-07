using UnityEngine;
using Commands.Settings;

public class SettingModel : Singleton<SettingModel>
{
    private SettingModel() { }
    public BindableProperty<int> BGMVolume = new BindableProperty<int>()
    {
        Value = 50
    };

    public BindableProperty<int> SFXVolume = new BindableProperty<int>()
    {
        Value = 50
    };

    public BindableProperty<int> ResolutionIndex = new BindableProperty<int>()
    {
        Value = 0
    };

    public BindableProperty<bool> IsFullscreen = new BindableProperty<bool>
    {
        Value = true
    };

    /// <summary>
    /// 将现在的设置项储存到Playerprefs中
    /// </summary>
    public void Save()
    {
        PlayerPrefs.SetInt("BGMVolume", BGMVolume.Value);
        PlayerPrefs.SetInt("SFXVolume", SFXVolume.Value);
        PlayerPrefs.SetInt("ResolutionIndex", ResolutionIndex.Value);
        PlayerPrefs.SetInt("IsFullscreen", IsFullscreen.Value ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 将Playerprefs中的设置项加载到游戏中
    /// </summary>
    public void Load()
    {
        BGMVolume.Value = PlayerPrefs.GetInt("BGMVolume", 50);
        SFXVolume.Value = PlayerPrefs.GetInt("SFXVolume", 50);
        ResolutionIndex.Value = PlayerPrefs.GetInt("ResolutionIndex", 0);
        IsFullscreen.Value = PlayerPrefs.GetInt("IsFullscreen", 1) == 1;


        new SetBGMVolumeCommand(BGMVolume.Value).Execute();
        new SetSFXVolumeCommand(SFXVolume.Value).Execute();
        new SetFullscreenCommand(IsFullscreen.Value).Execute();
        new SetResolutionCommand(ResolutionIndex.Value).Execute();
    }

}
