using Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : SingletonMono<MenuController>
{
    // �ڳ�������֮ǰ�Զ�ʵ��������
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoad()
    {
        var instance = Instance;  // ȷ�������ڳ�������֮ǰ������
    }

    private MenuModel model; // ���� MenuModel ʵ��

    // �ڳ������ֶ����Զ��������ͣ�˵�����
    public GameObject pauseMenu;

    // Unity�������ں������ڶ���ʵ����ʱ����
    void Awake()
    {
        Debug.Log("MenuController Awake ������"); // ��¼Awake���õ���־��Ϣ
        model = MenuModel.Instance;  // ��ȡ MenuModel ʵ��
        EventCenter.Instance.Subscribe("MenuChanged", TogglePause);  // ���Ĳ˵��л��¼�
    }

    // Unity�������ں������ڶ������ú����
    private void Start()
    {
        Debug.Log("MenuController Start ������"); // ��¼Start���õ���־��Ϣ
    }

    // ÿ֡���£����ڼ������
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  // ����Esc������
        {
            Debug.Log("��⵽ Esc ������");  // ������������־��Ϣ
            HandleEscKey();  // ����Esc�����߼�
        }
    }

    // ����Esc������ʱ���߼�
    void HandleEscKey()
    {
        // ��� UI ջΪ�գ���ʾ��ͣ�˵�
        if (model.IsUIStackEmpty())
        {
            // ���ز���ʾ��ͣ�˵�
            model.PushUI("Menu/PauseMenu");
        }
        else
        {
            // ���򷵻���һ���˵�
            new Commands.Menu.BackCommand().Execute();
        }
    }

    // �л���ͣ�ͻָ���Ϸ
    private void TogglePause()
    {
        if (model.IsUIStackEmpty())  // �ж� UI ջ�Ƿ�Ϊ��
        {
            ResumeGame();  // ���ջΪ�գ���ָ���Ϸ
        }
        else
        {
            PauseGame();  // ���ջ��Ϊ�գ�����ͣ��Ϸ
        }
    }

    // ��ͣ��Ϸ
    private void PauseGame()
    {
        Debug.Log("��Ϸ����ͣ");  // �����Ϸ��ͣ��־��Ϣ
        EventCenter.Instance.Publish("GamePaused");  // ������Ϸ��ͣ�¼�
        Time.timeScale = 0f;  // ����ʱ������Ϊ0����ͣ������Ϸ����
    }

    // �ָ���Ϸ
    private void ResumeGame()
    {
        Debug.Log("��Ϸ�ѻָ�");  // �����Ϸ�ָ���־��Ϣ
        EventCenter.Instance.Publish("GameResumed");  // ������Ϸ�ָ��¼�
        Time.timeScale = 1f;  // �ָ�ʱ�����ţ�������Ϸ����
    }
}
