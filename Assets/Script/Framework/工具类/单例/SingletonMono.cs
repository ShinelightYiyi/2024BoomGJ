using UnityEngine;
using System;



/// <summary>
/// �����ص�����ֻ���ڵ�һ�ε��õ�ʱ��Ż�ʵ������
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();

                        if (_instance == null)
                        {
                            var singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";

                            DontDestroyOnLoad(singletonObject);
                        }
                    }
                }
            }
            return _instance;
        }
    }
}
