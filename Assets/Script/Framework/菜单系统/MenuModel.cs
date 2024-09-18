using Framework;
using UnityEngine;
using System.Collections.Generic;

public class MenuModel : Singleton<MenuModel>
{
    // 私有构造函数，确保单例模式
    private MenuModel() { }

    // 使用堆栈管理 UI 界面的显示
    private Stack<GameObject> uiStack = new Stack<GameObject>();

    // 缓存已加载的菜单
    private Dictionary<string, GameObject> loadedUIs = new Dictionary<string, GameObject>();

    // 从Resources加载UI（如果尚未加载）
    private GameObject LoadUI(string path)
    {
        // 如果字典中还没有该路径的UI
        if (!loadedUIs.ContainsKey(path))
        {
            // 从Resources加载预制体
            GameObject uiPrefab = Resources.Load<GameObject>(path);
            if (uiPrefab != null)
            {
                // 实例化预制体
                GameObject uiInstance = GameObject.Instantiate(uiPrefab,GameObject.Find("UGUICanvas").transform);
                loadedUIs[path] = uiInstance;  // 加载的实例缓存到字典中
                uiInstance.SetActive(false);  // 默认隐藏加载的UI
            }
            else
            {
                Debug.LogError($"未找到路径 {path} 下的菜单预制体！");  // 输出资源加载失败的日志信息
            }
        }
        return loadedUIs[path];  // 返回加载的 UI 实例
    }

    // 添加 UI 并显示
    public void PushUI(string path)
    {
        GameObject ui = LoadUI(path);  // 加载UI
        if (ui != null)
        {
            if (uiStack.Count > 0)
            {
                // 隐藏当前栈顶的UI
                uiStack.Peek().SetActive(false);
            }

            // 将新的 UI 推入栈顶并显示
            uiStack.Push(ui);
            ui.SetActive(true);

            // 通知菜单发生变化
            EventCenter.Instance.Publish("MenuChanged");
        }
    }

    // 移除 UI
    public GameObject PopUI()
    {
        if (uiStack.Count > 0)
        {
            // 获取并移除栈顶的UI
            GameObject topUI = uiStack.Pop();
            topUI.SetActive(false);  // 隐藏当前 UI

            // 如果栈中还有其他 UI，则显示下一个 UI
            if (uiStack.Count > 0)
            {
                uiStack.Peek().SetActive(true);  // 显示下一个栈顶的UI
            }

            // 通知菜单变化
            EventCenter.Instance.Publish("MenuChanged");

            return topUI;  // 返回移除的UI
        }

        return null;  // 如果栈为空，返回 null
    }

    // 判断 UI 栈是否为空
    public bool IsUIStackEmpty()
    {
        return uiStack.Count == 0;  // 返回栈是否为空
    }
}
