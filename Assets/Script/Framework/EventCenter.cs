using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //为了使用Unity自带委托，必须要引用

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
/// 事件中心
/// </summary>
public class EventCenter:Singleton<EventCenter>
{
    private EventCenter() { }//防止被实例化

    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    
    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <typeparam name="T">参数</typeparam>
    /// <param name="name">事件名</param>
    /// <param name="action">监听事件（委托函数）</param>
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
    /// 添加事件监听（无参数）
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">监听事件（委托函数）</param>
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
    /// 移除事件监听
    /// </summary>
    /// <typeparam name="T">参数</typeparam>
    /// <param name="name">事件名</param>
    /// <param name="action">监听事件（委托函数）</param>
    public void UnSubscribe<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions -= action;
    }

    /// <summary>
    /// 移除事件监听（无参数）
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">监听事件（委托函数）</param>
    public void UnSubscribe(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions -= action;
    }

/// <summary>
/// 事件触发
/// </summary>
/// <typeparam name="T">参数</typeparam>
/// <param name="name">事件名</param>
/// <param name="info">参数</param>
    public void Publish<T>(string name, T info)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo<T>).actions != null)
                (eventDic[name] as EventInfo<T>).actions.Invoke(info);
        }
    }

/// <summary>
/// 事件触发（无参数）
/// </summary>
/// <param name="name">事件名</param>
    public void Publish(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo).actions != null)
                (eventDic[name] as EventInfo).actions.Invoke();
        }
    }

    /// <summary>
    /// 清空事件中心
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}


