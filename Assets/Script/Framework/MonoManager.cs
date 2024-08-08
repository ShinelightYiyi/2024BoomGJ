using UnityEngine;
using System.Collections;

public class MonoManager : SingletonMono<MonoManager>
{
    /// <summary>
    /// 执行协程
    /// </summary>
    /// <param name="routine">执行的协程</param>
    public void ExecuteCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    /// <summary>
    /// 停止协程
    /// </summary>
    /// <param name="routine">停止的协程</param>
    public void StopRoutine(IEnumerator routine)
    {
        StopCoroutine(routine);
    }

    /// <summary>
    /// 停止所有协程
    /// </summary>
    public void StopAllRoutinesManaged()
    {
        StopAllCoroutines();
    }
}
