using Framework;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuView : MonoBehaviour
{
    public Button backButton;           //���ذ�ť
    public Slider BGMSlider;            //BGM������
    public Slider SFXSlider;            //SFX������

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
