using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //Ϊ��ʹ��Unity�Դ�ί�У�����Ҫ����

public interface IEventInfo
{

}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}


/// <summary>
/// �¼�����
/// </summary>
public class EventCenter:Singleton<EventCenter>
{
    private EventCenter() { }//��ֹ��ʵ����

    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    
    /// <summary>
    /// ����¼�����
    /// </summary>
    /// <typeparam name="T">����</typeparam>
    /// <param name="name">�¼���</param>
    /// <param name="action">�����¼���ί�к�����</param>
    public void Subscribe<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo<T>(action));
        }
    }

    /// <summary>
    /// ����¼��������޲�����
    /// </summary>
    /// <param name="name">�¼���</param>
    /// <param name="action">�����¼���ί�к�����</param>
    public void Subscribe(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }

    /// <summary>
    /// �Ƴ��¼�����
    /// </summary>
    /// <typeparam name="T">����</typeparam>
    /// <param name="name">�¼���</param>
    /// <param name="action">�����¼���ί�к�����</param>
    public void UnSubscribe<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions -= action;
    }

    /// <summary>
    /// �Ƴ��¼��������޲�����
    /// </summary>
    /// <param name="name">�¼���</param>
    /// <param name="action">�����¼���ί�к�����</param>
    public void UnSubscribe(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions -= action;
    }

/// <summary>
/// �¼�����
/// </summary>
/// <typeparam name="T">����</typeparam>
/// <param name="name">�¼���</param>
/// <param name="info">����</param>
    public void Publish<T>(string name, T info)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo<T>).actions != null)
                (eventDic[name] as EventInfo<T>).actions.Invoke(info);
        }
    }

/// <summary>
/// �¼��������޲�����
/// </summary>
/// <param name="name">�¼���</param>
    public void Publish(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo).actions != null)
                (eventDic[name] as EventInfo).actions.Invoke();
        }
    }

    /// <summary>
    /// ����¼�����
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}


