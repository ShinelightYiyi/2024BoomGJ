using UnityEngine;
using System.Collections;

public class MonoManager : SingletonMono<MonoManager>
{
    /// <summary>
    /// ִ��Э��
    /// </summary>
    /// <param name="routine">ִ�е�Э��</param>
    public void ExecuteCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    /// <summary>
    /// ֹͣЭ��
    /// </summary>
    /// <param name="routine">ֹͣ��Э��</param>
    public void StopRoutine(IEnumerator routine)
    {
        StopCoroutine(routine);
    }

    /// <summary>
    /// ֹͣ����Э��
    /// </summary>
    public void StopAllRoutinesManaged()
    {
        StopAllCoroutines();
    }
}
