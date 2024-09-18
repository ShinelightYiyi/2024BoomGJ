using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : MonoBehaviour
{
    public Button backButton;           //���ذ�ť
    public Button saveButton;           //�浵��ť
    public Button settingsButton;       //���ð�ť
    public Button mainMenuButton;       //���˵���ť

    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        backButton.onClick.AddListener(() => new Commands.Menu.BackCommand().Execute());
        saveButton.onClick.AddListener(() => new Commands.VisualNovel.SaveCommand().Execute());
        settingsButton.onClick.AddListener(() => new Commands.Menu.OpenMenu("Menu/SettingMenu").Execute());
        mainMenuButton.onClick.AddListener(() => new Commands.Menu.LoadSceneCommand("MainMenu").Execute());
        
    }

}
