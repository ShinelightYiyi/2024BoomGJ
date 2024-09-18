using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : MonoBehaviour
{
    public Button backButton;           //返回按钮
    public Button saveButton;           //存档按钮
    public Button settingsButton;       //设置按钮
    public Button mainMenuButton;       //主菜单按钮

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
