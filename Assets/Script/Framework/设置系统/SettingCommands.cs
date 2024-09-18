using System.Collections;
using UnityEngine;
using Framework;

namespace Commands.Settings
{
    public struct SaveSettingsCommand : ICommand
    {
        public void Execute()
        {
            SettingModel.Instance.Save();
            Debug.Log("保存设置");
        }
    }

    public struct LoadSettingsCommand : ICommand
    {
        public void Execute()
        {
            SettingModel.Instance.Load();
            Debug.Log("加载设置");
        }
    }

    public struct SetBGMVolumeCommand : ICommand
    {
        private int volume;
        public SetBGMVolumeCommand(int volume)
        {
            this.volume = volume;
        }

        public void Execute()
        {
            SettingModel.Instance.BGMVolume.Value = volume;
            // 更新BGM音量
            Debug.Log($"BGM音量改为{volume}");
            AudioController.Instance.SetBGMVolume(volume);
        }
    }

    public struct SetSFXVolumeCommand : ICommand
    {
        private int volume;

        public SetSFXVolumeCommand(int volume)
        {
            this.volume = volume;
        }

        public void Execute()
        {
            SettingModel.Instance.SFXVolume.Value = volume;
            // 更新音效音量
            Debug.Log($"音效音量改为{volume}");
            AudioController.Instance.SetSFXVolume(volume);
        }
    }

    public struct SetFullscreenCommand : ICommand
    {
        private bool isFullscreen;

        public SetFullscreenCommand(bool isFullscreen)
        {
            this.isFullscreen = isFullscreen;
        }

        public void Execute()
        {
            SettingModel.Instance.IsFullscreen.Value = isFullscreen;
            Screen.fullScreen = isFullscreen;
        }
    }

    public struct SetResolutionCommand : ICommand
    {
        private int index;

        public SetResolutionCommand(int index)
        {
            this.index = index;

        }

        public void Execute()
        {
            SettingModel.Instance.ResolutionIndex.Value = index;
            Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, Screen.fullScreen);
        }
    }
}
