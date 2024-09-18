using Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : SingletonMono<MenuController>
{
    // 在场景加载之前自动实例化单例
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoad()
    {
        var instance = Instance;  // 确保单例在场景加载之前被创建
    }

    private MenuModel model; // 引用 MenuModel 实例

    // 在场景中手动或自动分配的暂停菜单引用
    public GameObject pauseMenu;

    // Unity生命周期函数，在对象实例化时调用
    void Awake()
    {
        Debug.Log("MenuController Awake 被调用"); // 记录Awake调用的日志信息
        model = MenuModel.Instance;  // 获取 MenuModel 实例
        EventCenter.Instance.Subscribe("MenuChanged", TogglePause);  // 订阅菜单切换事件
    }

    // Unity生命周期函数，在对象启用后调用
    private void Start()
    {
        Debug.Log("MenuController Start 被调用"); // 记录Start调用的日志信息
    }

    // 每帧更新，用于检测输入
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  // 监听Esc键按下
        {
            Debug.Log("检测到 Esc 键按下");  // 输出按键检测日志信息
            HandleEscKey();  // 处理Esc按键逻辑
        }
    }

    // 处理Esc键按下时的逻辑
    void HandleEscKey()
    {
        // 如果 UI 栈为空，显示暂停菜单
        if (model.IsUIStackEmpty())
        {
            // 加载并显示暂停菜单
            model.PushUI("Menu/PauseMenu");
        }
        else
        {
            // 否则返回上一个菜单
            new Commands.Menu.BackCommand().Execute();
        }
    }

    // 切换暂停和恢复游戏
    private void TogglePause()
    {
        if (model.IsUIStackEmpty())  // 判断 UI 栈是否为空
        {
            ResumeGame();  // 如果栈为空，则恢复游戏
        }
        else
        {
            PauseGame();  // 如果栈不为空，则暂停游戏
        }
    }

    // 暂停游戏
    private void PauseGame()
    {
        Debug.Log("游戏已暂停");  // 输出游戏暂停日志信息
        EventCenter.Instance.Publish("GamePaused");  // 发布游戏暂停事件
        Time.timeScale = 0f;  // 设置时间缩放为0，暂停所有游戏进程
    }

    // 恢复游戏
    private void ResumeGame()
    {
        Debug.Log("游戏已恢复");  // 输出游戏恢复日志信息
        EventCenter.Instance.Publish("GameResumed");  // 发布游戏恢复事件
        Time.timeScale = 1f;  // 恢复时间缩放，继续游戏进程
    }
}
