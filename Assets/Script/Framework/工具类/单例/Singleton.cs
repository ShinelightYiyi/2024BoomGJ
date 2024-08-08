using System;
using System.Reflection;
/// <summary>
/// µ¥Àý
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : Singleton<T>
{
    private static T _Instance;
    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                var type = typeof(T);
                var ctors=type.GetConstructors(BindingFlags.Instance|BindingFlags.NonPublic);
                var ctor=Array.Find(ctors,c=>c.GetParameters().Length==0);
                if (ctors == null)
                {
                    throw new Exception("Non Public Constructor Not Found in" + type.Name);
                }
                _Instance=ctor.Invoke(null)as T;
            }
            return _Instance;
        }
    }
}
