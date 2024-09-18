using Framework;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuView : MonoBehaviour
{
    public Button backButton;           //返回按钮
    public Slider BGMSlider;            //BGM音量条
    public Slider SFXSlider;            //SFX音量条

    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        backButton.onClick.AddListener(() => new Commands.Menu.BackCommand().Execute());
        BGMSlider.onValueChanged.AddListener((a)=> new Commands.Settings.SetBGMVolumeCommand((int)a).Execute());
        SFXSlider.onValueChanged.AddListener((a) => new Commands.Settings.SetSFXVolumeCommand((int)a).Execute());
    }

}
