
using System.Collections;
using UnityEngine;

/// <summary>
/// 设置控制器
/// </summary>
public class SettingsController : SingletonMono<SettingsController>
{
    public SettingModel model;
    //public SettingsView view;

    private void Start()
    {
        model = SettingModel.Instance;
        // 初始化视图
        /*var resolutions = GetAvailableResolutions();
        view.Initialize(model.BGMVolume, model.SFXVolume, model.IsFullscreen, resolutions);

        // 订阅UI事件
        view.bgmVolumeSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        view.sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        view.fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
        view.resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);*/
    }

}


