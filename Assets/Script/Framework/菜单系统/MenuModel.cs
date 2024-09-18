using Framework;
using UnityEngine;
using System.Collections.Generic;

public class MenuModel : Singleton<MenuModel>
{
    // ˽�й��캯����ȷ������ģʽ
    private MenuModel() { }

    // ʹ�ö�ջ���� UI �������ʾ
    private Stack<GameObject> uiStack = new Stack<GameObject>();

    // �����Ѽ��صĲ˵�
    private Dictionary<string, GameObject> loadedUIs = new Dictionary<string, GameObject>();

    // ��Resources����UI�������δ���أ�
    private GameObject LoadUI(string path)
    {
        // ����ֵ��л�û�и�·����UI
        if (!loadedUIs.ContainsKey(path))
        {
            // ��Resources����Ԥ����
            GameObject uiPrefab = Resources.Load<GameObject>(path);
            if (uiPrefab != null)
            {
                // ʵ����Ԥ����
                GameObject uiInstance = GameObject.Instantiate(uiPrefab,GameObject.Find("UGUICanvas").transform);
                loadedUIs[path] = uiInstance;  // ���ص�ʵ�����浽�ֵ���
                uiInstance.SetActive(false);  // Ĭ�����ؼ��ص�UI
            }
            else
            {
                Debug.LogError($"δ�ҵ�·�� {path} �µĲ˵�Ԥ���壡");  // �����Դ����ʧ�ܵ���־��Ϣ
            }
        }
        return loadedUIs[path];  // ���ؼ��ص� UI ʵ��
    }

    // ��� UI ����ʾ
    public void PushUI(string path)
    {
        GameObject ui = LoadUI(path);  // ����UI
        if (ui != null)
        {
            if (uiStack.Count > 0)
            {
                // ���ص�ǰջ����UI
                uiStack.Peek().SetActive(false);
            }

            // ���µ� UI ����ջ������ʾ
            uiStack.Push(ui);
            ui.SetActive(true);

            // ֪ͨ�˵������仯
            EventCenter.Instance.Publish("MenuChanged");
        }
    }

    // �Ƴ� UI
    public GameObject PopUI()
    {
        if (uiStack.Count > 0)
        {
            // ��ȡ���Ƴ�ջ����UI
            GameObject topUI = uiStack.Pop();
            topUI.SetActive(false);  // ���ص�ǰ UI

            // ���ջ�л������� UI������ʾ��һ�� UI
            if (uiStack.Count > 0)
            {
                uiStack.Peek().SetActive(true);  // ��ʾ��һ��ջ����UI
            }

            // ֪ͨ�˵��仯
            EventCenter.Instance.Publish("MenuChanged");

            return topUI;  // �����Ƴ���UI
        }

        return null;  // ���ջΪ�գ����� null
    }

    // �ж� UI ջ�Ƿ�Ϊ��
    public bool IsUIStackEmpty()
    {
        return uiStack.Count == 0;  // ����ջ�Ƿ�Ϊ��
    }
}
